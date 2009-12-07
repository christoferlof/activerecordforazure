using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ActiveRecordForAzure.Core {
    public class ActiveRecordTestContext : IActiveRecordContext  {
        
        public static ActiveRecordTestContext Initialize() {
            if (ActiveRecordContext.Current == null) {
                ActiveRecordContext.Initialize(new ActiveRecordTestContext());
            }
            return ActiveRecordContext.Current as ActiveRecordTestContext;
        }

        private readonly Hashtable _entities = new Hashtable();
        private readonly Hashtable _setups = new Hashtable();

        public IQueryable<TEntity> CreateQuery<TEntity>() where TEntity : new() {
            
            EnsureEntityList<TEntity>();
            return GetList<TEntity>().AsQueryable();

        }

        public void AddEntity<TEntity>(TEntity entity) where TEntity : new() {
            EnsureEntityList<TEntity>();
            AddEntityToList(entity);
        }

        public void RegisterSetup<TEntity>(ActiveRecordTestSetup<TEntity> setup) {
            _setups[GetKey<TEntity>()] = setup;
        }

        public ActiveRecordTestSetup<TEntity> GetSetup<TEntity>(){
            return _setups[GetKey<TEntity>()] as ActiveRecordTestSetup<TEntity>;
        }

        private void AddEntityToList<TEntity>(TEntity entity) {
            GetList<TEntity>().Add(entity);
        }

        private List<TEntity> GetList<TEntity>() {
            return (List<TEntity>)_entities[GetKey<TEntity>()];
        }

        private void EnsureEntityList<TEntity>() where TEntity : new() {

            if(ShouldCreateEntityList<TEntity>()) {
                CreateEntityList<TEntity>();

                if(EntityHasSetups<TEntity>()) {
                    SetupEntity<TEntity>();
                }
            }
        }

        private void SetupEntity<TEntity>() where TEntity : new() {
            var setup = _setups[GetKey<TEntity>()] as ActiveRecordTestSetup<TEntity>;
                    
            for(var i = 0; i<setup.NumberOfEntities;i++) {
                var entity = new TEntity();

                foreach (var member in entity.GetType().GetProperties()) {
                    if (member.PropertyType == typeof(string)) {
                        member.SetValue(entity, string.Format("{0}-{1}", member.Name, i + 1), null);
                    }
                }

                AddEntityToList(entity);
            }
        }

        private bool EntityHasSetups<TEntity>() {
            return _setups[GetKey<TEntity>()] != null;
        }

        private bool ShouldCreateEntityList<TEntity>() {
            return !_entities.ContainsKey(GetKey<TEntity>());
        }

        private void CreateEntityList<TEntity>() {
            _entities.Add(GetKey<TEntity>(), new List<TEntity>());
        }

        private Type GetKey<TEntity>() {
            return typeof (TEntity);
        }

    }
}