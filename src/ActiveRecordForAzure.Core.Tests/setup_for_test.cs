using System;
using System.Linq;
using Xunit;

namespace ActiveRecordForAzure.Core.Tests {
    public class setup_for_test {

        public setup_for_test() {
            FakeEntity.Setup(1);
        }

        [Fact]
        public void it_creates_the_specified_number_of_entity_instances() {

            var actual = ActiveRecordContext.Current.CreateQuery<FakeEntity>().Count();

            Assert.Equal(1, actual);

        }

        [Fact]
        public void it_creates_entities_having_their_properties_set_to_defaults() {

            var entity = GetFakeEntities().FirstOrDefault();

            Assert.Equal("ForeignKey-1", entity.ForeignKey); //[MemberName]-[Counter] for strings

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

        private static IQueryable<FakeEntity> GetFakeEntities() {
            return ActiveRecordContext.Current.CreateQuery<FakeEntity>();
        }
    }
}
