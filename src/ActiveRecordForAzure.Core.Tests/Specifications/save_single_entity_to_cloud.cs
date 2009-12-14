using ActiveRecordForAzure.Core.Abstractions;
using Moq;
using Xunit;

namespace ActiveRecordForAzure.Core.Tests.Specifications {
    public class save_single_entity_to_cloud : CloudSpecification {
        private readonly FakeEntity _entity;

        public save_single_entity_to_cloud() {
            _entity = new FakeEntity();
            _entity.Save();
        }

        [Fact]
        public void it_should_add_entity_to_context() {
            TableContext.Verify(x => x.AddObject(It.IsAny<string>(), _entity));
        }

        [Fact]
        public void it_should_submit_changes_to_cloud() {
            TableContext.Verify(x => x.SaveChanges());
        }

    }
}