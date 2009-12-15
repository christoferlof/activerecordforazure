using System.Linq;

namespace ActiveRecordForAzure.Core {

    /// <summary>
    /// The ActiveRecord Context
    /// </summary>
    public interface IActiveRecordContext {

        /// <summary>
        /// Creates the query.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns></returns>
        IQueryable<TEntity> CreateQuery<TEntity>() where TEntity : ActiveRecord<TEntity>, new();

        /// <summary>
        /// Adds the entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        void AddEntity<TEntity>(TEntity entity) where TEntity : ActiveRecord<TEntity>, new();

        /// <summary>
        /// Updates the entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        void UpdateEntity<TEntity>(TEntity entity) where TEntity : ActiveRecord<TEntity>, new();

        /// <summary>
        /// Deletes the entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        void DeleteEntity<TEntity>(TEntity entity) where TEntity : ActiveRecord<TEntity>, new();
    }
}