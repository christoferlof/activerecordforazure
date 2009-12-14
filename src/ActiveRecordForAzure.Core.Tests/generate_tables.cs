using System.Text;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;
using Xunit;

namespace ActiveRecordForAzure.Core.Tests {
    public class generate_tables {

        public generate_tables() {
            TableStorage.EnsureTables(GetType());
        }

        [Fact]
        public void it_should_pluralize_common_subjects() {
            Assert.True(TableStorage.Tables.Contains("Customers"));
        }

        [Fact]
        public void it_should_pluralize_common_subjects_ending_with_y() {
            Assert.True(TableStorage.Tables.Contains("Parties"));
        }

        public class Customer : ActiveRecord<Customer> {}

        public class Party : ActiveRecord<Party> { }
    }
}
