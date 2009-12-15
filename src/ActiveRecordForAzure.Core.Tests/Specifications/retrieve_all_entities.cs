using System.Linq;
using Xunit;

namespace ActiveRecordForAzure.Core.Tests.Specifications {
    public class retrieve_all_entities {

        [Fact]
        public void it_returns_all() {
            FakeEntity.Setup(10);
            var entities = FakeEntity.All();
            Assert.Equal(10, entities.Count());
        }

    }
}
