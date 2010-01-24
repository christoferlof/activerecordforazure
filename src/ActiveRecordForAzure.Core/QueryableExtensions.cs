using System.Data.Services.Client;
using System.Linq;
using Microsoft.WindowsAzure.StorageClient;

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

        /// <summary>
        /// Determines whether the query instance is a cloud query (<see cref="DataServiceQuery{TElement}"/>) or not.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="self">extension method</param>
        /// <returns>
        /// 	<c>true</c> if the instance is a <see cref="DataServiceQuery{TElement}"/> otherwise, <c>false</c>.
        /// </returns>
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

        /// <summary>
        /// Executes the query and returns a paged list of entities
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="self">The self.</param>
        /// <returns></returns>
        public static IPagedList<TEntity> ToPagedList<TEntity>(this IQueryable<TEntity> self) 
            where TEntity : ActiveRecord<TEntity>, new() {
            
            if (self.IsCloudQuery()) {
                return CloudPagedList(self);
            }
            return LinqPagedList(self);
        }

        private static IPagedList<TEntity> LinqPagedList<TEntity>(IQueryable<TEntity> self) 
            where TEntity : ActiveRecord<TEntity>, new() {
            
            var list = self.ToList();
            var last = list[list.Count - 1] as ActiveRecord<TEntity>;
            var lastToken = new PageToken(last.RowKey, last.PartitionKey);

            var hasNextPage = ActiveRecordContext.Current
                .CreateQuery<TEntity>()
                .Skip(lastToken.Token)
                .Take(2);

            if(hasNextPage.Count() == 2) {
                var next = hasNextPage.Single(x => x.RowKey != last.RowKey);
                return new PagedList<TEntity>(list, 
                    new PageToken(next.RowKey,next.PartitionKey));
            } else {
                return new PagedList<TEntity>(list,null);
            }
        }

        private static IPagedList<TEntity> CloudPagedList<TEntity>(IQueryable<TEntity> self) {
            var result = (QueryOperationResponse<TEntity>) ((DataServiceQuery<TEntity>) self).Execute();
            return new PagedList<TEntity>(result, new PageToken(result.Headers));
        }
    }
}
