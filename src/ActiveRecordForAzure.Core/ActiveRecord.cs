 using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.WindowsAzure.StorageClient;

namespace ActiveRecordForAzure.Core {

    /// <summary>
    /// The ActiveRecord base class
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public class ActiveRecord<TEntity> : TableServiceEntity where TEntity : ActiveRecord<TEntity>, new() {
        
        private const string DefaultPartitionKey = "AR4A";

        /// <summary>
        /// Returns all entities
        /// </summary>
        /// <returns></returns>
        public static IList<TEntity> All() {
            var query = ActiveRecordContext.Current.CreateQuery<TEntity>();
            if(query!=null)
                return query.ToList();
            return null;
        }

        /// <summary>
        /// Finds the specified entity.
        /// </summary>
        /// <param name="rowKey">The id.</param>
        /// <returns></returns>
        public static TEntity Find(string rowKey) {
            if (!string.IsNullOrEmpty(rowKey))
                return Find(x => (x.RowKey == rowKey && x.PartitionKey == DefaultPartitionKey)).FirstOrDefault();
            return null;
        }

        /// <summary>
        /// Finds and returns the entities matching the criteria
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        public static IList<TEntity> Find(Expression<Func<TEntity,bool>> criteria) {
            return ActiveRecordContext.Current
                .CreateQuery<TEntity>()
                .Where(criteria)
                .ToList();
        }

        /// <summary>
        /// Returns the given number of entities
        /// </summary>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        public static IPagedList<TEntity> Paged(int pageSize) {
            return ActiveRecordContext.Current
                .CreateQuery<TEntity>()
                .Take(pageSize)
                .ToPagedList();
        }


        /// <summary>
        /// Returns the given number of entities
        /// </summary>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="pageToken">The page token of the page to get</param>
        /// <returns></returns>
        public static IPagedList<TEntity> Paged(int pageSize, string pageToken) {
            return ActiveRecordContext.Current
                .CreateQuery<TEntity>()
                .Skip(pageToken)
                .Take(pageSize)
                .ToPagedList();
        }

        /// <summary>
        /// Setups the specified number of entities for test.
        /// </summary>
        /// <param name="numberOfEntities">The number of entities.</param>
        /// <returns></returns>
        public static ActiveRecordTestSetup<TEntity> Setup(int numberOfEntities) {
            return new ActiveRecordTestSetup<TEntity>(numberOfEntities);
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="rowKey">The row key.</param>
        public static void Delete(string rowKey) {
            Delete(Find(rowKey));
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public static void Delete(TEntity entity) {
            ActiveRecordContext.Current.DeleteEntity(entity);
        }

        /// <summary>
        /// Deletes this instance.
        /// </summary>
        public void Delete() {
            Delete((TEntity) this);
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        /// <remarks>If the RowKey or the PartitionKey is empty; the entity will be treated as new (create).</remarks>
        public void Save() {

            if (IsNew()) {
                SetRowKey();
                SetPartitionKey();
                ActiveRecordContext.Current.AddEntity((TEntity)this);
            } else {
                ActiveRecordContext.Current.UpdateEntity((TEntity)this);
            }
        }

        /// <summary>
        /// Determines whether this instance is new.
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if this instance is new; otherwise, <c>false</c>.
        /// </returns>
        public bool IsNew() {
            return (string.IsNullOrEmpty(RowKey) || string.IsNullOrEmpty(PartitionKey));
        }

        private void SetPartitionKey() {
                PartitionKey = DefaultPartitionKey;
        }

        private void SetRowKey() {
                RowKey = string.Format("{0:10}", DateTime.MaxValue.Ticks - DateTime.UtcNow.Ticks);
        }

        
    }
}