using Moq;
using Xunit;

namespace ActiveRecordForAzure.Core.Tests.Specifications {
    public class delete_entity_from_cloud : CloudSpecification {
        
        public delete_entity_from_cloud() {

            var entity = new FakeEntity() {RowKey = "xyz"};
            entity.Delete();
        }

        [Fact]
        public void it_should_add_entity_to_context() {
            TableContext.Verify(x => x.DeleteObject(It.Is<FakeEntity>(e => e.RowKey == "xyz")));
        }

        [Fact]
        public void it_should_submit_changes_to_cloud() {
            TableContext.Verify(x => x.SaveChanges());
        }

    }
}
