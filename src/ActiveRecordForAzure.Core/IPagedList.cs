using System.Collections.Generic;

namespace ActiveRecordForAzure.Core {
    public interface IPagedList<T> : IList<T> {
        PageToken NextPageToken { get; }
    }
}
