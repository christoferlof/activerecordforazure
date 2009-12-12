using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ActiveRecordForAzure.Core.Abstractions;
using Moq;
using Xunit;

namespace ActiveRecordForAzure.Core.Tests {
    public class save_single_entity_to_cloud {

        private readonly Mock<ITableServiceContext> _tableContext;
        private readonly FakeEntity _entity;

        public save_single_entity_to_cloud() {
            
            _tableContext = new Mock<ITableServiceContext>();
            ActiveRecordContext.Initialize(new ActiveRecordContext(_tableContext.Object));

            _entity = new FakeEntity();

            _entity.Save();
        }

        [Fact]
        public void it_should_add_entity_to_context() {
            _tableContext.Verify(x => x.AddObject(It.IsAny<string>(), _entity));
        }

        [Fact]
        public void it_should_submit_changes_to_cloud() {
            _tableContext.Verify(x => x.SaveChanges());
        }

    }
}
