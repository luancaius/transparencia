using MongoDB.Driver;

namespace Repository;

public static class Util
{
    public static void createUniqueIndexForTable<T>(IMongoDatabase database, string tableName, string indexName)
    {
        var indexKeys = Builders<T>.IndexKeys.Ascending(indexName);
        var indexOptions = new CreateIndexOptions { Unique = true };
        var indexModel = new CreateIndexModel<T>(indexKeys, indexOptions);
        var collection = database.GetCollection<T>(tableName);
        collection.Indexes.CreateOne(indexModel);    
    }
}