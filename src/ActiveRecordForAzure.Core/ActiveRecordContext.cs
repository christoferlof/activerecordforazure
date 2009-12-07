using System;
using System.Linq;
using Microsoft.WindowsAzure.StorageClient;

namespace ActiveRecordForAzure.Core {
    public class ActiveRecordContext : IActiveRecordContext {

        private static IActiveRecordContext _context;

        public static IActiveRecordContext Current {
            get {
                return _context;
            }
        }

        public IQueryable<TEntity> CreateQuery<TEntity>() where TEntity : new() {
            return null;
        }

        public static void Initialize(IActiveRecordContext context) {
            _context = context;
        }

        public void AddEntity<TEntity>(TEntity entity) where TEntity : new() {
            throw new NotImplementedException();
        }
    }
}