using Moq;
using Xunit;

namespace ActiveRecordForAzure.Core.Tests.Specifications {
    public class generate_tables {

        private readonly Mock<ITableProvider> _tableProvider;

        public generate_tables() {

            _tableProvider = new Mock<ITableProvider>();
            var tableStorage = new TableStorage(_tableProvider.Object);
            
            tableStorage.EnsureTables(GetType());
        }

        [Fact]
        public void it_should_pluralize_common_subjects() {
            _tableProvider.Verify(x => x.CreateTableIfNotExists("Customers"));
        }

        [Fact]
        public void it_should_pluralize_common_subjects_ending_with_y() {
            _tableProvider.Verify(x => x.CreateTableIfNotExists("Parties"));
        }

        public class Customer : ActiveRecord<Customer> {}

        public class Party : ActiveRecord<Party> {}
    }
}