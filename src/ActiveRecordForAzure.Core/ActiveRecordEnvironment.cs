using System;

namespace ActiveRecordForAzure.Core {

    /// <summary>
    /// Configures and starts the ActiveRecord Environment
    /// </summary>
    public static class ActiveRecordEnvironment {

        /// <summary>
        /// Starts the ActiveRecord Environment
        /// </summary>
        /// <param name="objectInAssemblyContainingTheEntities">An object instance from the assembly containing the entities to create tables for</param>
        public static void Start(object objectInAssemblyContainingTheEntities) {
            Start(objectInAssemblyContainingTheEntities.GetType());
        }

        /// <summary>
        /// Starts the ActiveRecord Environment
        /// </summary>
        /// <param name="typeInAssemblyContainingTheEntities">A type from the assembly containing the entities to create tables for</param>
        public static void Start(Type typeInAssemblyContainingTheEntities) {
            var tableStorage = new TableStorage(new CloudTableProvider());
            tableStorage.EnsureTables(typeInAssemblyContainingTheEntities);
        }

    }
}
