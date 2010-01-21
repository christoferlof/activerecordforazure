using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace ActiveRecordForAzure.Core.Tests.Specifications {
    public class paged_list {
        
        private readonly PagedList<string> _pagedList;

        public paged_list() {
            var list = new List<string>() {
                                              {"foo"},
                                              {"bar"}
                                          };
            var nextPageToken = new PageToken("row@partition");

            _pagedList = new PagedList<string>(list, nextPageToken);

        }

        [Fact]
        public void it_initializes_from_enumerable_and_page_token() {

            Assert.IsAssignableFrom(typeof(IPagedList<string>), _pagedList);
        }

        [Fact]
        public void it_contains_the_provided_number_of_entities() {
            
            Assert.Equal(2, _pagedList.Count);
        }

        [Fact]
        public void it_contains_the_provided_next_page_row() {
            
            Assert.Equal("row", _pagedList.NextPageToken.RowKey);
        }

        [Fact]
        public void it_contains_the_provided_next_page_partition() {

            Assert.Equal("partition", _pagedList.NextPageToken.PartitionKey);
        }
    }
}
