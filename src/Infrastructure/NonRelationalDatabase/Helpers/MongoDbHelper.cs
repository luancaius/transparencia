using MongoDB.Driver;

namespace NonRelationalDatabase.Helpers;

public class MongoDbHelper
{
    private readonly IMongoDatabase _database;

    public MongoDbHelper(string connectionString, string databaseName)
    {
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
    }

    public void InsertData<T>(string collectionName, T document)
    {
        var collection = _database.GetCollection<T>(collectionName);
        collection.InsertOne(document);
    }
    
    public List<T> GetData<T>(string collectionName, FilterDefinition<T> filter = null)
    {
        var collection = _database.GetCollection<T>(collectionName);
        filter ??= Builders<T>.Filter.Empty;
        return collection.Find(filter).ToList();
    }
}