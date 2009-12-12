using System;
using System.Linq;
using ActiveRecordForAzure.Core.Abstractions;

namespace ActiveRecordForAzure.Core {
    public class ActiveRecordContext : IActiveRecordContext {

        #region statics

        public static void Initialize(IActiveRecordContext context) {
            _context = context;
        }

        private static IActiveRecordContext _context;

        public static IActiveRecordContext Current {
            get {
                return _context; //TODO: "auto" initialization ?
            }
        }

        #endregion

        private readonly ITableServiceContext _dataContext;

        public ActiveRecordContext(ITableServiceContext dataContext) {
            _dataContext = dataContext;
        }

        public IQueryable<TEntity> CreateQuery<TEntity>() where TEntity : new() {
            return _dataContext.CreateQuery<TEntity>("Messages");
        }

        public void AddEntity<TEntity>(TEntity entity) where TEntity : new() {
            _dataContext.AddObject("Messages", entity);
            _dataContext.SaveChanges();
        }
    }
}