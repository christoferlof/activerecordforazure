namespace ActiveRecordForAzure.Core {

    /// <summary>
    /// Provides access to the Table Store
    /// </summary>
    public interface ITableProvider {

        /// <summary>
        /// Creates the table if it not exists.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <returns></returns>
        bool CreateTableIfNotExists(string tableName);
    }
}