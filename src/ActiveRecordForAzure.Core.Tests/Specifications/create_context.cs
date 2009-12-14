using System;
using Moq;
using Xunit;

namespace ActiveRecordForAzure.Core.Tests.Specifications {
    public class create_context {

        [Fact]
        public void it_should_use_registered_factory() {

            var expected = new Mock<IActiveRecordContext>().Object;
            ActiveRecordContextFactory.RegisterFactory(() => expected);

            var actual = ActiveRecordContextFactory.CreateContext();

            Assert.Same(expected,actual);

            ActiveRecordContextFactory.RegisterFactory(null); //clean up

        }

        [Fact]
        public void it_should_use_the_cloud_factory_if_no_other_factory_is_registered() {

            //TODO: abstract the cloudstorageaccount and refactor
            Assert.Throws(typeof (InvalidOperationException), () => ActiveRecordContextFactory.CreateContext());

            ActiveRecordContextFactory.RegisterFactory(null); //clean up

        }

    }
}