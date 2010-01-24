using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace ActiveRecordForAzure.Core.Tests.Specifications {
    public class page_token_from_row_and_partition {

        private readonly PageToken _token;

        public page_token_from_row_and_partition() {
            
            _token = new PageToken("row","partition");

        }

        [Fact]
        public void it_sets_row_properly() {
            Assert.Equal("row",_token.RowKey);
        }

        [Fact]
        public void it_sets_partition_properly() {
            Assert.Equal("partition",_token.PartitionKey);
        }

        [Fact]
        public void it_returns_correct_token_string() {
            Assert.Equal("row@partition",_token.Token);
        }

    }
}
