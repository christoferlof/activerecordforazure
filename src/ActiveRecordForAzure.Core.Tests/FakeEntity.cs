using System;

namespace ActiveRecordForAzure.Core.Tests {
    public class FakeEntity : ActiveRecord<FakeEntity> {

        public string ForeignKey { get; set; }

        public string Name { get; set; }

        public DateTime DateTimeValue { get; set; }

        public bool BoolValue { get; set; }

        public int IntValue { get; set; }

        public long LongValue { get; set; }

        public Guid GuidValue { get; set; }

        public double DoubleValue { get; set; }

        public byte[] ByteValue { get; set; }

    }
}