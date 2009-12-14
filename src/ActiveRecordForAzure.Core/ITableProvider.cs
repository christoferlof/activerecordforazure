namespace ActiveRecordForAzure.Core {
    public interface ITableProvider {
        bool CreateTableIfNotExists(string tableName);
    }
}