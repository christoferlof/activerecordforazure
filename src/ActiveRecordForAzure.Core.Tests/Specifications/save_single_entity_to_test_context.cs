using System.Diagnostics;
using System.Linq;
using Xunit;

namespace ActiveRecordForAzure.Core.Tests.Specifications {
    public class save_single_entity_to_test_context : FakeEntitySpecification {
        private ActiveRecordTestContext _context;

        public save_single_entity_to_test_context() {
            _context = new ActiveRecordTestContext();
        }

        [Fact]
        public void it_stores_added_entity() {

            _context.AddEntity(new FakeEntity());

            Assert.Equal(1, _context.CreateQuery<FakeEntity>().Count());

        }

        [Fact]
        public void it_updates_updated_entity() {

            var entity = new FakeEntity() {RowKey = "rowkey", PartitionKey = "partitionkey"};
            _context.AddEntity(entity);

            entity.Name = "new name";
            _context.UpdateEntity(entity);

            Assert.Equal(1, _context.CreateQuery<FakeEntity>().Count());
            Assert.Equal("new name", _context.CreateQuery<FakeEntity>().First().Name);
        }
    }
}
