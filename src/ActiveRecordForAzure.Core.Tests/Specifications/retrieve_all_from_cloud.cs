using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using Xunit;
using System.Data.Services.Client;

namespace ActiveRecordForAzure.Core.Tests.Specifications {
    public class retrieve_all_from_cloud : CloudSpecification {

        public retrieve_all_from_cloud() {
            FakeEntity.All();
        }

        [Fact]
        public void it_creates_query() {
            TableContext.Verify(x => x.CreateQuery<FakeEntity>(It.IsAny<string>()));
        }

    }
}
