using System.Linq;
using Xunit;

namespace ActiveRecordForAzure.Core.Tests {
    public class save_single_entity {

        [Fact]
        public void test_context_stores_added_entity() {

            var context = new ActiveRecordTestContext();

            context.AddEntity(new FakeEntity());

            Assert.Equal(1, context.CreateQuery<FakeEntity>().Count());

        }

    }
}
