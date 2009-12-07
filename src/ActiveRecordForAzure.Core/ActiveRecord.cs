using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.WindowsAzure.StorageClient;

namespace ActiveRecordForAzure.Core {
    public class ActiveRecord<TEntity> : TableServiceEntity where TEntity : TableServiceEntity, new() {

        /// <summary>
        /// Finds the specified entity.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public static TEntity Find(string id) {
            if(!string.IsNullOrEmpty(id))
                return new TEntity() {RowKey = id};
            return null;
        }

        public static IList<TEntity> Find(Expression<Func<TEntity,bool>> predicate) {
            return ActiveRecordContext.Current.CreateQuery<TEntity>().Where(predicate).ToList();
        }

        public static ActiveRecordTestSetup<TEntity> Setup(int numberOfEntities) {

            return new ActiveRecordTestSetup<TEntity>(numberOfEntities);
        }
    }
}