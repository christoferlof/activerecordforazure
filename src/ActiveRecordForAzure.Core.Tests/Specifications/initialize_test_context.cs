using Xunit;
using System;

namespace ActiveRecordForAzure.Core.Tests.Specifications {
    public class initialize_test_context : IDisposable {

        [Fact]
        public void it_initializes_test_context() {
            
            ActiveRecordTestContext.EnsureTestContext();

            var context = ActiveRecordContext.Current as ActiveRecordTestContext;

            Assert.NotNull(context);
        }

        public void Dispose() {
            ActiveRecordContext.Destroy();
        }
    }
}