using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace ActiveRecordForAzure.Core.Tests.Specifications {
    public class setup_multiple_for_test {

        public setup_multiple_for_test() {
            
            FakeEntity.Setup(1);
            FakeEntity.Setup(2);
            
            Customer.Setup(3);
            Order.Setup(4);
        }

        [Fact]
        public void it_should_overwrite_existing_setups_for_same_type_of_entity() {

            Assert.Equal(2, Count<FakeEntity>()); 

        }

        [Fact]
        public void it_should_not_overwrite_existing_setups_for_other_types_of_entities() {

            Assert.Equal(4, Count<Order>());
        }

        [Fact]
        public void it_should_not_overwrite_existing_setups_for_other_types_of_entities2() {

            Assert.Equal(3, Count<Customer>());
        }

        private int Count<TEntity>() where TEntity : ActiveRecord<TEntity>, new() {
            return ActiveRecordContext.Current.CreateQuery<TEntity>().Count();
        }

        private class Customer : ActiveRecord<Customer> {}

        private class Order : ActiveRecord<Order> { }
    }
}
