namespace ActiveRecordForAzure.Core.Tests {
    internal class FakeEntity : ActiveRecord<FakeEntity> {

        public string ForeignKey { get; set; }

    }
}