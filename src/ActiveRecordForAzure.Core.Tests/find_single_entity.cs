﻿using Xunit;

namespace ActiveRecordForAzure.Core.Tests {
    public class find_single_entity {

        [Fact]
        public void returns_instance_when_found() {

            FakeEntity.Setup(2)
                .With(x => x.RowKey).Returns("{0}");

            var id = "1";
            var entity = FakeEntity.Find(id);

            Assert.Equal(id, entity.RowKey);
            
        }

        [Fact]
        public void returns_null_when_not_found() {

            FakeEntity.Setup(1)
                .With(x => x.RowKey).Returns("key");

            var id = "fookey";
            var entity = FakeEntity.Find(id);

            Assert.Equal(null, entity);

        }
    }
}
