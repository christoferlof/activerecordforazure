using System.Linq;

namespace ActiveRecordForAzure.Core.Tests {
    public class FakeEntitySpecification {
        
        protected FakeEntitySpecification() {
            ActiveRecordTestContext.Initialize();
        }

        protected static FakeEntity FirstEntity() {
            return GetFakeEntities().First();
        }

        protected static IQueryable<FakeEntity> GetFakeEntities() {

            return ActiveRecordContext.Current.CreateQuery<FakeEntity>();

        }
    }
}
