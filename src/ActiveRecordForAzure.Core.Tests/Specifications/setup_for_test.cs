using System;
using System.Linq;
using Xunit;

namespace ActiveRecordForAzure.Core.Tests.Specifications {
    public class setup_for_test : FakeEntitySpecification {

        public setup_for_test() {
            FakeEntity.Setup(1);
        }

        [Fact]
        public void it_creates_the_specified_number_of_entity_instances() {
            var actual = GetFakeEntities().Count();
            Assert.Equal(1, actual);
        }

        [Fact]
        public void it_initializes_non_specified_string_member_with_default() {
            Assert.Equal("RowKey-1", FirstEntity().RowKey);
        }

        [Fact]
        public void it_initializes_non_specified_datetime_member_with_minvalue() {
            Assert.Equal(DateTime.MinValue, FirstEntity().DateTimeValue);
        }

        [Fact]
        public void it_initializes_non_specified_bool_member_with_false() {
            Assert.Equal(false, FirstEntity().BoolValue);
        }

        [Fact]
        public void it_initializes_non_specified_int_member_with_zero() {
            Assert.Equal(0, FirstEntity().IntValue);
        }

        [Fact]
        public void it_initializes_non_specified_long_member_with_zero() {
            Assert.Equal(0, FirstEntity().LongValue);
        }

        [Fact]
        public void it_initializes_non_specified_guid_member_with_empty() {
            Assert.Equal(Guid.Empty, FirstEntity().GuidValue);
        }

        [Fact]
        public void it_initializes_non_specified_double_member_with_zero() {
            Assert.Equal(0.0, FirstEntity().DoubleValue);
        }

        [Fact]
        public void it_initializes_non_specified_byte_member_with_null() {
            Assert.Equal(null, FirstEntity().ByteValue);
        }

        [Fact]
        public void it_initialized_the_test_context() {

            var context = ActiveRecordContext.Current;

            Assert.Equal(typeof(ActiveRecordTestContext), context.GetType());

        }

        [Fact]
        public void test_setup_registers_with_context() {

            var setup = new ActiveRecordTestSetup<FakeEntity>(1);

            var context = ActiveRecordContext.Current as ActiveRecordTestContext;

            Assert.Equal(setup, context.GetSetup<FakeEntity>());

        }

        [Fact]
        public void it_clears_the_setup_once_its_been_setup() {
            var entity = FirstEntity(); //access the entities to trigger the loading
            var context = ActiveRecordContext.Current as ActiveRecordTestContext;

            Assert.Null(context.GetSetup<FakeEntity>());
        }
    }
}