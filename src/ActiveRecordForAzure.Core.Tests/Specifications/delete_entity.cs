using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace ActiveRecordForAzure.Core.Tests.Specifications {
    public class delete_entity {

        [Fact]
        public void it_is_removed_from_test_context() {

            var rowkey = "rowkey";

            FakeEntity.Setup(1)
                .With(x => x.RowKey).Returns(rowkey)
                .With(x => x.PartitionKey).Returns("AR4A"); //TODO: fix hard code

            FakeEntity.Delete(rowkey);

            var count = ((ActiveRecordTestContext) ActiveRecordContext.Current).CreateQuery<FakeEntity>().Count();

            Assert.Equal(0, count);

        }
    }
}
