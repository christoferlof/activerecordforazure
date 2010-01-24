using System.Collections.Generic;

namespace ActiveRecordForAzure.Core {

    /// <summary>
    /// A paged list of entities
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public class PagedList<TEntity> : List<TEntity>, IPagedList<TEntity>{

        /// <summary>
        /// Initializes a new instance of the <see cref="PagedList&lt;TEntity&gt;"/> class.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <param name="nextPageToken">The next page token.</param>
        public PagedList(IEnumerable<TEntity> entities, PageToken nextPageToken) {
            AddRange(entities);
            NextPageToken = nextPageToken;
        }

        /// <summary>
        /// Gets or sets the next page token.
        /// </summary>
        /// <value>The next page token.</value>
        public PageToken NextPageToken {
            get; private set;
        }
    }
}