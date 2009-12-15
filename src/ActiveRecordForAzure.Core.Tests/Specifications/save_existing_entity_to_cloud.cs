using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using Xunit;

namespace ActiveRecordForAzure.Core.Tests.Specifications {
    public class save_existing_entity_to_cloud : CloudSpecification {

        private readonly FakeEntity _entity;

        public save_existing_entity_to_cloud() {
            _entity = new FakeEntity {
                RowKey = "rowkey",
                PartitionKey = "partitionkey"
            };
            _entity.Save();
        }

        [Fact]
        public void it_should_update_entity_to_context() {
            TableContext.Verify(x => x.UpdateObject(_entity));
        }

        [Fact]
        public void it_should_submit_changes_to_cloud() {
            TableContext.Verify(x => x.SaveChanges());
        }

    }
}
