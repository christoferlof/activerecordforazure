using System;

namespace ActiveRecordForAzure.Core {
    public static class ActiveRecordEnvironment {

        public static void Start(object objectInAssemblyContainingTheEntities) {
            Start(objectInAssemblyContainingTheEntities.GetType());
        }

        public static void Start(Type typeInAssemblyContainingTheEntities) {
            var tableStorage = new TableStorage(new CloudTableProvider());
            tableStorage.EnsureTables(typeInAssemblyContainingTheEntities);
        }

    }
}
