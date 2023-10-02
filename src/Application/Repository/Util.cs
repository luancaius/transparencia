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
    
    public static void createUniqueIndexForTable<T>(IMongoDatabase database, string tableName, params string[] indexNames)
    {
        var indexKeysBuilder = Builders<T>.IndexKeys;
        var indexKeys = indexKeysBuilder.Combine(indexNames.Select(name => indexKeysBuilder.Ascending(name)));
        var indexOptions = new CreateIndexOptions { Unique = true };
        var indexModel = new CreateIndexModel<T>(indexKeys, indexOptions);
        var collection = database.GetCollection<T>(tableName);
        collection.Indexes.CreateOne(indexModel);
    }
}