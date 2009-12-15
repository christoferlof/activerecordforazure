using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ActiveRecordForAzure.Core {
    public class ActiveRecordTestContext : IActiveRecordContext  {

        public static void EnsureTestContext() {
            ActiveRecordContextFactory.RegisterFactory(() => new ActiveRecordTestContext());
        }

        private readonly Hashtable _entities = new Hashtable();
        private readonly Hashtable _setups = new Hashtable();

        public IQueryable<TEntity> CreateQuery<TEntity>() where TEntity : ActiveRecord<TEntity>, new() {
            EnsureEntityList<TEntity>();
            return GetList<TEntity>().AsQueryable();
        }

        public void AddEntity<TEntity>(TEntity entity) where TEntity : ActiveRecord<TEntity>, new() {
            EnsureEntityList<TEntity>();
            AddEntityToList(entity);
        }

        public void UpdateEntity<TEntity>(TEntity entity) where TEntity : ActiveRecord<TEntity>, new() {
            var existing = CreateQuery<TEntity>().FirstOrDefault(
                x => x.RowKey == entity.RowKey && x.PartitionKey == entity.PartitionKey);
            if (existing != null) {
                existing = entity;
            }
        }

        public void DeleteEntity<TEntity>(TEntity entity) where TEntity : ActiveRecord<TEntity>, new() {
            var list = GetList<TEntity>();
            list.Remove(entity);
        }

        public void RegisterSetup<TEntity>(ActiveRecordTestSetup<TEntity> setup) where TEntity : new() {
            CreateEntityList<TEntity>();
            _setups[GetKey<TEntity>()] = setup;
        }

        public ActiveRecordTestSetup<TEntity> GetSetup<TEntity>() where TEntity : new() {
            return _setups[GetKey<TEntity>()] as ActiveRecordTestSetup<TEntity>;
        }

        private void AddEntityToList<TEntity>(TEntity entity) {
            GetList<TEntity>().Add(entity);
        }

        private List<TEntity> GetList<TEntity>() {
            return (List<TEntity>)_entities[GetKey<TEntity>()];
        }

        private void EnsureEntityList<TEntity>() where TEntity : new() {

            if (ShouldCreateEntityList<TEntity>()) {
                CreateEntityList<TEntity>();
            }

            if (EntityHasSetups<TEntity>()) {
                SetupEntity<TEntity>();
                ClearSetups<TEntity>();
            }
        }

        private bool EntityHasSetups<TEntity>() {
            return _setups[GetKey<TEntity>()] != null;
        }
        private void ClearSetups<TEntity>() {
            _setups[GetKey<TEntity>()] = null;
        }

        private bool ShouldCreateEntityList<TEntity>() {
            return (_entities[GetKey<TEntity>()] == null);
        }

        private void CreateEntityList<TEntity>() where TEntity : new() {
            _entities[GetKey<TEntity>()] = new List<TEntity>();
        }
         
        private string GetKey<TEntity>() {
            var type = typeof (TEntity);
            if (type.IsGenericType)
                type = type.GetGenericArguments()[0];
            return type.FullName;
        }


        private void SetupEntity<TEntity>() where TEntity : new() {
            
            var setup = _setups[GetKey<TEntity>()] as ActiveRecordTestSetup<TEntity>;
                    
            for(var i = 0; i<setup.NumberOfEntities;i++) {
                var entity = new TEntity();

                foreach (var member in entity.GetType().GetProperties()) {
                    SetupMember(i, setup, entity, member);
                }

                AddEntityToList(entity);
            }
        }

        private void SetupMember<TEntity>(int counter, ActiveRecordTestSetup<TEntity> setup, TEntity entity, PropertyInfo member
            ) where TEntity : new() {
            
            //registered returns
            var memberSetup = setup.Members.SingleOrDefault(m => m.Name == member.Name);
                    
            if(memberSetup != null) {
                SetupExplicitMember(member, memberSetup, counter, entity);
            }
            else{
                SetupDefaultMember(member, entity, counter);
            }
        }

        private void SetupExplicitMember<TEntity>(PropertyInfo member, ActiveRecordTestSetupMember memberSetup, int counter, TEntity entity) {
            if (member.PropertyType == typeof(string)) {
                memberSetup.Value = string.Format(memberSetup.Value.ToString(), counter + 1);
            }
            member.SetValue(entity, memberSetup.Value, null);
        }

        private void SetupDefaultMember<TEntity>(PropertyInfo member, TEntity entity, int counter) {
            if (member.PropertyType == typeof(string)) {
                member.SetValue(entity, string.Format("{0}-{1}", member.Name, counter + 1), null);
            }
        }

        
    }
}