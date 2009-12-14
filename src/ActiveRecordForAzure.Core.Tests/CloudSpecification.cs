using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ActiveRecordForAzure.Core.Abstractions;
using Moq;

namespace ActiveRecordForAzure.Core.Tests {
    public class CloudSpecification : IDisposable {
        private readonly Mock<ITableServiceContext> _tableContext;

        public CloudSpecification() {
            _tableContext = new Mock<ITableServiceContext>();
            ActiveRecordContextFactory.RegisterFactory(() => new ActiveRecordContext(_tableContext.Object));
            ActiveRecordContext.Initialize();
        }

        public Mock<ITableServiceContext> TableContext {
            get { return _tableContext; }
        }

        public void Dispose() {
            ActiveRecordContext.Destroy();
        }
    }
}
