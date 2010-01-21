using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace ActiveRecordForAzure.Core.Tests.Specifications
{
    public class page_entities
    {
        public page_entities() {
            FakeEntity.Setup(9)
                .With(x => x.RowKey).Returns("{0}")
                .With(x => x.PartitionKey).Returns("partition");
        }

        [Fact]
        public void it_returns_the_given_number_of_entities() {

            var pageSize = 5;

            var entities = FakeEntity.Paged(pageSize);

            Assert.Equal(pageSize, entities.Count());
        }

        [Fact]
        public void it_returns_a_page_token_for_the_next_page()
        {
            var pageSize = 5;

            var entities = FakeEntity.Paged(pageSize);

            Assert.Equal("6@partition", entities.NextPageToken.Token);
        }

        [Fact]
        public void it_returns_the_given_number_of_entities_for_given_page() {

            var pageToken = "6@partition"; //RowKey@PartitionKey

            var entities = FakeEntity.Paged(5, pageToken);

            Assert.Equal(4, entities.Count());
        }

        [Fact]
        public void skip_throws_argument_exception_when_empty_token_is_provided() {

            var list = FakeEntity.All().AsQueryable();

            Assert.Throws<ArgumentException>(() => QueryableExtensions.Skip(list, string.Empty)); 
        }

        [Fact]
        public void skip_throw_argument_exception_when_invalid_token_is_provided() {
            
            var list = FakeEntity.All().AsQueryable();

            Assert.Throws<ArgumentException>(() => QueryableExtensions.Skip(list, "fail")); 
        }
    }
}
