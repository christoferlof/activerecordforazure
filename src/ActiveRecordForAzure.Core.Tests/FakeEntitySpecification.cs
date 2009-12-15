using System.Linq;
using System;

namespace ActiveRecordForAzure.Core.Tests {
    public class FakeEntitySpecification : IDisposable {

        public FakeEntitySpecification() {
            ActiveRecordTestContext.EnsureTestContext();
        }

        protected static FakeEntity FirstEntity() {
            return GetFakeEntities().First();
        }

        protected static IQueryable<FakeEntity> GetFakeEntities() {

            return ActiveRecordContext.Current.CreateQuery<FakeEntity>();

        }

        public void Dispose() {
            ActiveRecordContext.Destroy();
        }
    }
}
