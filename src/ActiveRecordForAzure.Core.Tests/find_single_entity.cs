using Xunit;

namespace ActiveRecordForAzure.Core.Tests {
    public class find_single_entity {

        [Fact]
        public void returns_instance_when_found() {

            var id = "id";
            var entity = FakeEntity.Find(id);

            Assert.Equal(id, entity.RowKey);
            
        }

        [Fact]
        public void returns_null_when_not_found() {

            var id = string.Empty;
            var entity = FakeEntity.Find(id);

            Assert.Equal(null, entity);

        }
    }
}
