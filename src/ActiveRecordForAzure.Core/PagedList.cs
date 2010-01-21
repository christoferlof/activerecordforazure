using System.Collections.Generic;

namespace ActiveRecordForAzure.Core {
    
    public class PagedList<TEntity> : List<TEntity>, IPagedList<TEntity>{
        public PagedList(IEnumerable<TEntity> entities, PageToken nextPageToken) {
            AddRange(entities);
            NextPageToken = nextPageToken;
        }

        public PageToken NextPageToken {
            get; private set;
        }
    }
}