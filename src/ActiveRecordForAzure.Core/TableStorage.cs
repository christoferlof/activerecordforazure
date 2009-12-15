using System;
using System.Collections.Generic;
using System.Linq;

namespace ActiveRecordForAzure.Core {

    /// <summary>
    /// Performs operations on the provided table store
    /// </summary>
    public class TableStorage {

        private readonly ITableProvider _tableProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="TableStorage"/> class.
        /// </summary>
        /// <param name="tableProvider">The table provider.</param>
        public TableStorage(ITableProvider tableProvider) {
            _tableProvider = tableProvider;
        }

        /// <summary>
        /// Ensures the tables.
        /// </summary>
        /// <param name="typeInAssemblyContainingTheEntities">The type in an assembly containing the entities to create tables for</param>
        public void EnsureTables(Type typeInAssemblyContainingTheEntities) {
            
            var activeRecords = GetActiveRecords(typeInAssemblyContainingTheEntities);

            foreach (var entity in activeRecords) {
                _tableProvider.CreateTableIfNotExists(entity.GetTableName());
            }
        }

        private static IEnumerable<Type> GetActiveRecords(Type typeInAssemblyContainingTheEntities) {
            return typeInAssemblyContainingTheEntities.Assembly.GetTypes().Where(
                x => x.BaseType.IsGenericType &&
                     x.BaseType.GetGenericTypeDefinition() == typeof(ActiveRecord<>));
        }
    }
}