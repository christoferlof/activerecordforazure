using System.Linq;
using System;

namespace ActiveRecordForAzure.Core.Tests {
    public class FakeEntitySpecification {

        public FakeEntitySpecification() {
            ActiveRecordTestContext.EnsureTestContext();
        }

        protected static FakeEntity FirstEntity() {
            return GetFakeEntities().First();
        }

        protected static IQueryable<FakeEntity> GetFakeEntities() {

            return ActiveRecordContext.Current.CreateQuery<FakeEntity>();

        }
    }
}
