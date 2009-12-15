using System.Linq;
using ActiveRecordForAzure.Core.Abstractions;
using Moq;
using Xunit;

namespace ActiveRecordForAzure.Core.Tests.Specifications {
    public class save_single_entity : FakeEntitySpecification {
        
        [Fact]
        public void it_saves_the_entity_to_active_context() {
            when_an_entity_is_saved();
            Assert.Equal(1, GetFakeEntities().Count());
        }

        [Fact]
        public void it_sets_the_rowkey_if_its_empty() {
            var entity = when_an_entity_is_saved();
            Assert.True(!string.IsNullOrEmpty(entity.RowKey));
        }

        [Fact]
        public void it_sets_the_partitionkey_if_its_empty() {
            var entity = when_an_entity_is_saved();
            
            Assert.True(!string.IsNullOrEmpty(entity.PartitionKey));
        }

        private static FakeEntity when_an_entity_is_saved() {
            var entity = new FakeEntity();
            entity.Save();
            return entity;
        }

        [Fact]
        public void it_is_added_if_partitionkey_is_empty() {

            FakeEntity.Setup(0);

            new FakeEntity {RowKey = "rowkey"}.Save();

            Assert.Equal(1, GetFakeEntities().Count());
        }

        [Fact]
        public void it_is_added_if_rowkey_is_empty() {

            FakeEntity.Setup(0);

            new FakeEntity { PartitionKey = "partitionkey" }.Save();

            Assert.Equal(1, GetFakeEntities().Count());
        }

        [Fact]
        public void it_is_updated_if_rowkey_and_partitionkey_are_set() {

            FakeEntity.Setup(1);

            var entity = FirstEntity();
            entity.RowKey = "rowkey";
            entity.PartitionKey = "partitionkey";
            entity.Save();

            Assert.Equal(1, GetFakeEntities().Count());
        }
    }
}