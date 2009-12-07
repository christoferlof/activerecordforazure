using System.Linq;

namespace ActiveRecordForAzure.Core {
    public interface IActiveRecordContext {

        IQueryable<TEntity> CreateQuery<TEntity>() where TEntity : new();

        void AddEntity<TEntity>(TEntity entity) where TEntity : new();
    }
}