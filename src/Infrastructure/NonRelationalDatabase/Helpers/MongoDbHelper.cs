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
    
    public List<T> GetData<T>(string collectionName, FilterDefinition<T>? filter = null)
    {
        try
        {
            var collection = _database.GetCollection<T>(collectionName);
            filter ??= Builders<T>.Filter.Empty;
            var result =  collection.Find(filter);
            if (result.Any())
            {
                return result.ToList();
            }
            return new List<T>();
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
    
    public bool DeleteData<T>(string collectionName, FilterDefinition<T>? filter)
    {
        try
        {
            var collection = _database.GetCollection<T>(collectionName);
            var result = collection.DeleteOne(filter);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error deleting data: {e.Message}");
            return false;
        }
    }

}