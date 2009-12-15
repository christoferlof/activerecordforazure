using System;
using System.Linq;
using ActiveRecordForAzure.Core.Abstractions;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;

namespace ActiveRecordForAzure.Core {
    public class ActiveRecordContext : IActiveRecordContext {

        #region statics

        protected static IActiveRecordContext Context;

        public static IActiveRecordContext Current {
            get {
                EnsureContext();
                return Context;
            }
        }

        public static void Initialize() {
            Initialize(ActiveRecordContextFactory.CreateContext());
        }

        public static void Initialize(IActiveRecordContext context) {
            Context = context;
        }

        public static void Destroy() {
            Initialize(null);
        }

        protected static void EnsureContext() {
            if (Context == null)
                Initialize();
        }

        #endregion

        private readonly ITableServiceContext _dataContext;

        public ActiveRecordContext(ITableServiceContext dataContext) {
            _dataContext = dataContext;
        }

        public IQueryable<TEntity> CreateQuery<TEntity>() where TEntity : ActiveRecord<TEntity>, new() {
            return _dataContext.CreateQuery<TEntity>(typeof(TEntity).GetTableName()); //AsTableServiceQuery()
        }

        public void AddEntity<TEntity>(TEntity entity) where TEntity : ActiveRecord<TEntity>, new() {
            _dataContext.AddObject(typeof(TEntity).GetTableName(), entity);
            _dataContext.SaveChanges();
        }

        public void UpdateEntity<TEntity>(TEntity entity) where TEntity : ActiveRecord<TEntity>, new() {
            _dataContext.UpdateObject(entity);
            _dataContext.SaveChanges();
        }

        public void DeleteEntity<TEntity>(TEntity entity) where TEntity : ActiveRecord<TEntity>, new() {
            _dataContext.DeleteObject(entity);
            _dataContext.SaveChanges();
        }
    }
}