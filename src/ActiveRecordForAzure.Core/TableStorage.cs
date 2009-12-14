using System;
using System.Collections.Generic;
using System.Linq;

namespace ActiveRecordForAzure.Core {
    public class TableStorage {

        private readonly ITableProvider _tableProvider;

        public TableStorage(ITableProvider tableProvider) {
            _tableProvider = tableProvider;
        }

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