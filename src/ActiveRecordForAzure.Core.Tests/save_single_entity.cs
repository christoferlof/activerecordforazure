using System.Linq;
using Xunit;

namespace ActiveRecordForAzure.Core.Tests {
    public class save_single_entity : FakeEntitySpecification {

        [Fact]
        public void test_context_stores_added_entity() {

            var context = new ActiveRecordTestContext();

            context.AddEntity(new FakeEntity());

            Assert.Equal(1, context.CreateQuery<FakeEntity>().Count());

        }

        [Fact]
        public void it_saves_the_entity_to_active_context() {

            var entity = new FakeEntity();

            entity.Save();

            Assert.Equal(1, GetFakeEntities().Count());

        }
    }
}
