using System;

namespace ActiveRecordForAzure.Core {
    public static class ActiveRecordExtensions {

        private static string GetTableName(string entity) {
            if (!entity.EndsWith("y"))
                return entity + "s";
            return entity.Substring(0, entity.Length - 1) + "ies";
        }

        public static string GetTableName<TEntity>(this ActiveRecord<TEntity> self) where TEntity : ActiveRecord<TEntity>, new() {
            return GetTableName(self.GetType().Name);
        }

        public static string GetTableName(this Type self) {
            return GetTableName(self.Name);
        }
    }
}
