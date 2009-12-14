using System;
using ActiveRecordForAzure.Core.Abstractions;
using Moq;
using Xunit;

namespace ActiveRecordForAzure.Core.Tests.Specifications {
    public class initialize_context : IDisposable {

        [Fact]
        public void it_initializes_with_provided_context() {
            
            var context = new Mock<IActiveRecordContext>().Object;
            ActiveRecordContext.Initialize(context);

            Assert.Same(ActiveRecordContext.Current,context);
        }

        [Fact]
        public void it_defaults_to_null() {
            Assert.Null(TestableActiveRecordContext.TestableContext);
        }

        [Fact]
        public void it_initializes_on_first_access_to_current() {
            
            var context = new Mock<IActiveRecordContext>().Object;
            ActiveRecordContextFactory.RegisterFactory(() => context);

            Assert.NotNull(ActiveRecordContext.Current);
        }

        [Fact]
        public void it_returns_the_same_instance_the_second_time_its_accessed() {

            var context = new Mock<IActiveRecordContext>().Object;
            ActiveRecordContextFactory.RegisterFactory(() => context);

            var context1 = ActiveRecordContext.Current;
            var context2 = ActiveRecordContext.Current;

            Assert.Same(context1,context2);
        }

        private class TestableActiveRecordContext : ActiveRecordContext {
            private TestableActiveRecordContext(ITableServiceContext dataContext) : base(dataContext) {}

            public static IActiveRecordContext TestableContext {
                get { return Context; }
            }
        }

        public void Dispose() {
            ActiveRecordContext.Destroy();
        }
    }
}