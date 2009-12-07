using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace ActiveRecordForAzure.Core.Tests {
    public class initialize_context {

        [Fact]
        public void it_initializes_with_given_context_instance() {

            ActiveRecordContext.Initialize(new ActiveRecordTestContext());
 
            Assert.Equal(typeof(ActiveRecordTestContext), ActiveRecordContext.Current.GetType());
        }

        [Fact]
        public void test_context_initializes_itself() {
            
            ActiveRecordTestContext.Initialize();

            Assert.Equal(typeof(ActiveRecordTestContext), ActiveRecordContext.Current.GetType());
        }
          

    }
}
