using System;
using System.Linq;
using Xunit;

namespace ActiveRecordForAzure.Core.Tests.Specifications {
    public class setup_for_test_with_returns : FakeEntitySpecification {

        private readonly Guid _guidValue;

        public setup_for_test_with_returns() {

            _guidValue = Guid.NewGuid();

            FakeEntity.Setup(2)
                .With(x => x.RowKey).Returns("id-{0}")
                .With(x => x.Name).Returns("The Name")
                .With(x => x.ByteValue).Returns(new byte[2])
                .With(x => x.BoolValue).Returns(true)
                .With(x => x.DateTimeValue).Returns(DateTime.Today)
                .With(x => x.DoubleValue).Returns(double.MaxValue)
                .With(x => x.GuidValue).Returns(_guidValue)
                .With(x => x.IntValue).Returns(int.MaxValue)
                .With(x => x.LongValue).Returns(long.MaxValue);
        }

        [Fact]
        public void it_creates_the_specified_number_of_entities() {
            Assert.Equal(2, GetFakeEntities().Count());
        }

        [Fact]
        public void it_initializes_specified_string_member() {
            Assert.Equal("The Name", FirstEntity().Name);
        }

        [Fact]
        public void it_initializes_specified_string_member_with_tokens() {
            Assert.Equal("id-1", FirstEntity().RowKey);
        }

        [Fact]
        public void it_initializes_specified_string_member_with_tokens2() {
            Assert.IsAssignableFrom(typeof(FakeEntity), 
                GetFakeEntities().Where(x => x.RowKey == "id-2").First());
        }

        [Fact]
        public void it_initializes_specified_byte_member() {
            Assert.Equal(new byte[2], FirstEntity().ByteValue);
        }

        [Fact]
        public void it_initializes_specified_bool_member() {
            Assert.Equal(true, FirstEntity().BoolValue);
        }

        [Fact]
        public void it_initializes_specified_datetime_member() {
            Assert.Equal(DateTime.Today, FirstEntity().DateTimeValue);
        }

        [Fact]
        public void it_initializes_specified_double_member() {
            Assert.Equal(double.MaxValue, FirstEntity().DoubleValue);
        }

        [Fact]
        public void it_initializes_specified_guid_member() {
            Assert.Equal(_guidValue, FirstEntity().GuidValue);
        }

        [Fact]
        public void it_initializes_specified_int_member() {
            Assert.Equal(int.MaxValue, FirstEntity().IntValue);
        }

        [Fact]
        public void it_initializes_specified_long_member() {
            Assert.Equal(long.MaxValue, FirstEntity().LongValue);
        }

        [Fact]
        public void it_registers_member_and_value_with_setup() {

            var context = ActiveRecordContext.Current as ActiveRecordTestContext;
            var memberSetup = context.GetSetup<FakeEntity>().Members.Where(x => x.Name == "Name").FirstOrDefault();

            Assert.Equal("Name", memberSetup.Name);
            Assert.Equal("The Name", memberSetup.Value);

        }
    }
}