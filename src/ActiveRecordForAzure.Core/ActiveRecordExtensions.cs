using System;

namespace ActiveRecordForAzure.Core {

    /// <summary>
    /// ActiveRecord Extensions Methods
    /// </summary>
    public static class ActiveRecordExtensions {

        private static string GetTableName(string entity) {
            if (!entity.EndsWith("y"))
                return entity + "s";
            return entity.Substring(0, entity.Length - 1) + "ies";
        }

        /// <summary>
        /// Gets the name of the table from the entity
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string GetTableName<TEntity>(this ActiveRecord<TEntity> self) where TEntity : ActiveRecord<TEntity>, new() {
            return GetTableName(self.GetType().Name);
        }

        /// <summary>
        /// Gets the name of the table from the type
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string GetTableName(this Type self) {
            return GetTableName(self.Name);
        }
    }
}
