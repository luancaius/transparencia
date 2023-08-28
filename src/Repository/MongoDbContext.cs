using MongoDB.Driver;

namespace Repository;

public class MongoDbContext
{
    private readonly IMongoClient _mongoClient;
    public IMongoDatabase Database { get; }

    public MongoDbContext(string connectionString, string databaseName)
    {
        _mongoClient = new MongoClient(connectionString);
        Database = _mongoClient.GetDatabase(databaseName);
    }
}