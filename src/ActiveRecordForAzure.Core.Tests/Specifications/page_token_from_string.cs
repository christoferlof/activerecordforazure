using Xunit;

namespace ActiveRecordForAzure.Core.Tests.Specifications {
    public class page_token_from_string {
        
        private readonly PageToken _token;
        private readonly string _tokenString;

        public page_token_from_string() {

            _tokenString = "row@partition";
            _token = new PageToken(_tokenString);
        }

        [Fact]
        public void it_initializes_from_token_string() {

            Assert.Equal(_tokenString, _token.Token);
        }

        [Fact]
        public void it_parses_the_rowkey() {
            Assert.Equal("row", _token.RowKey);
        }

        [Fact]
        public void it_parses_the_partitionkey() {
            Assert.Equal("partition", _token.PartitionKey);
        }
    }
}