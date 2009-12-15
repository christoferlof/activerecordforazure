 using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.WindowsAzure.StorageClient;

namespace ActiveRecordForAzure.Core {

    public class ActiveRecord<TEntity> : TableServiceEntity where TEntity : ActiveRecord<TEntity>, new() {

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
                return Find(x => (x.RowKey == rowKey && x.PartitionKey == "AR4A")).FirstOrDefault(); //TODO: remove hard code
            return null;
        }

        public static IList<TEntity> Find(Expression<Func<TEntity,bool>> predicate) {
            return ActiveRecordContext.Current.CreateQuery<TEntity>().Where(predicate).ToList();
        }

        public static ActiveRecordTestSetup<TEntity> Setup(int numberOfEntities) {
            return new ActiveRecordTestSetup<TEntity>(numberOfEntities);
        }

        public static void Delete(string rowKey) {
            Delete(Find(rowKey));
        }

        public static void Delete(TEntity entity) {
            ActiveRecordContext.Current.DeleteEntity(entity);
        }

        public void Delete() {
            Delete((TEntity) this);
        }

        public void Save() {

            if (IsNew()) {
                SetRowKey();
                SetPartitionKey();
                ActiveRecordContext.Current.AddEntity((TEntity)this);
            } else {
                ActiveRecordContext.Current.UpdateEntity((TEntity)this);
            }
        }

        public bool IsNew() {
            return (string.IsNullOrEmpty(RowKey) || string.IsNullOrEmpty(PartitionKey));
        }

        private void SetPartitionKey() {
                PartitionKey = "AR4A";  //TODO: remove hard code
        }

        private void SetRowKey() {
                RowKey = string.Format("{0:10}", DateTime.MaxValue.Ticks - DateTime.Now.Ticks); //TODO: offer rowkey styles
        }

        
    }
}