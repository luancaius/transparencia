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
        try
        {
            var collection = _database.GetCollection<T>(collectionName);
            collection.InsertOne(document);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public List<T> GetData<T>(string collectionName, FilterDefinition<T> filter = null)
    {
        try
        {
            var collection = _database.GetCollection<T>(collectionName);
            filter ??= Builders<T>.Filter.Empty;
            return collection.Find(filter).ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public void UpsertData<T>(string collectionName, FilterDefinition<T> filter, T document)
    {
        try
        {
            var collection = _database.GetCollection<T>(collectionName);
            var options = new ReplaceOptions { IsUpsert = true };
            collection.ReplaceOne(filter, document, options);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    //delete data
    public void DeleteData<T>(string collectionName, T entity)
    {
        try
        {
            var collection = _database.GetCollection<T>(collectionName);
            var filter = Builders<T>.Filter.Eq("Id", entity);
            collection.DeleteOne(filter);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}