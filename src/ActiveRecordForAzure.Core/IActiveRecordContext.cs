using System.Linq;

namespace ActiveRecordForAzure.Core {
    public interface IActiveRecordContext {

        IQueryable<TEntity> CreateQuery<TEntity>() where TEntity : ActiveRecord<TEntity>, new();

        void AddEntity<TEntity>(TEntity entity) where TEntity : ActiveRecord<TEntity>, new();

        void UpdateEntity<TEntity>(TEntity entity) where TEntity : ActiveRecord<TEntity>, new();
        
        void DeleteEntity<TEntity>(TEntity entity) where TEntity : ActiveRecord<TEntity>, new();
    }
}