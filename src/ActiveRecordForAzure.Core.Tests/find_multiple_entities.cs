using System.Linq;
using Xunit;

namespace ActiveRecordForAzure.Core.Tests {
    public class find_multiple_entities {

        [Fact]
        public void using_expression_returnes_matching_entities() {

            FakeEntity
                .Setup(2);
                //.With(x => x.ForeignKey).Returns("foreign");
                
            var entities = FakeEntity.Find(x => x.ForeignKey == "ForeignKey-2"); //default pattern is "[MemberName]-[N]"

            Assert.Equal(1, entities.Count());

        }
    }
}
