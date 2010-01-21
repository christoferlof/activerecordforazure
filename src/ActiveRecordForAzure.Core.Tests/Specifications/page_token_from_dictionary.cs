using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace ActiveRecordForAzure.Core.Tests.Specifications {
    
    public class page_token_from_dictionary {
        
        private readonly PageToken _token;

        public page_token_from_dictionary() {
            var headers = new Dictionary<string, string>() {
               {"x-ms-continuation-NextPartitionKey", "PartitionKey"},
               {"x-ms-continuation-NextRowKey", "RowKey"}
            };
            _token = new PageToken(headers);
        }

        [Fact]
        public void it_initializes_from_header_dictionary() {

            Assert.Equal("RowKey@PartitionKey", _token.Token);
        }

    }
}
