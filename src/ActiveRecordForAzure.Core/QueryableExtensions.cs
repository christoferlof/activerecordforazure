using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Linq;
using System.Text;

namespace ActiveRecordForAzure.Core {

    /// <summary>
    /// Extentions Methods for querying
    /// </summary>
    public static class QueryableExtensions {
        
        

        /// <summary>
        /// Bypasses all entities prior the given Page Token 
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="self">The query.</param>
        /// <param name="pageToken">The page token.</param>
        /// <returns></returns>
        public static IQueryable<TEntity> Skip<TEntity>(this IQueryable<TEntity> self, string pageToken)
            where TEntity : ActiveRecord<TEntity>, new() {

            var token = new PageToken(pageToken);

            if (self.IsCloudQuery()) {
                return CloudSkip(self, token);
            }
            return LinqSkip(self, token);
        }

        public static bool IsCloudQuery<TEntity>(this IQueryable<TEntity> self) {
            return (self is DataServiceQuery<TEntity>);
        }

        private static IQueryable<TEntity> CloudSkip<TEntity>(
            IQueryable<TEntity> self, PageToken pageToken) {
            
            return ((DataServiceQuery<TEntity>)self)
                .AddQueryOption("NextPartitionKey", pageToken.PartitionKey)
                .AddQueryOption("NextRowKey", pageToken.RowKey);
        }

        private static IQueryable<TEntity> LinqSkip<TEntity>(
            IQueryable<TEntity> self, PageToken pageToken)
            where TEntity : ActiveRecord<TEntity>, new() {

            return self
                .SkipWhile(x => x.PartitionKey != pageToken.PartitionKey)
                .SkipWhile(x => x.RowKey != pageToken.RowKey);
        }

        public static IPagedList<TEntity> ToPagedList<TEntity>(this IQueryable<TEntity> self) where TEntity : ActiveRecord<TEntity>, new() {
           
            if (self.IsCloudQuery()) {
                var result = (QueryOperationResponse<TEntity>) ((DataServiceQuery<TEntity>) self).Execute();
                return new PagedList<TEntity>(result, new PageToken(result.Headers));
            }

            //how to know about the next row and partition?

            var list = self.ToList();
            var last = list[list.Count - 1] as ActiveRecord<TEntity>;


            return new PagedList<TEntity>(list, 
                new PageToken(last.RowKey + "@" + last.PartitionKey));
        }

    }
}
