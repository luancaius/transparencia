using MongoDB.Bson;
using MongoDB.Driver;
using Repository.JsonEntity;

namespace Repository;

public class JsonRepository
{
    private readonly IMongoClient _mongoClient;
    private readonly IMongoDatabase _database;

    public JsonRepository(string connectionString, string databaseName)
    {
        _mongoClient = new MongoClient(connectionString);
        _database = _mongoClient.GetDatabase(databaseName);
    }
    
    public IMongoCollection<Api1DeputadoDtoMongo> GetEntitiesCollection()
    {
        return _database.GetCollection<Api1DeputadoDtoMongo>($"temp_api1_deputados");
    }

    public async Task<Api1DeputadoDtoMongo> GetByIdAsync(ObjectId id)
    {
        var collection = GetEntitiesCollection();
        return await collection.Find(e => e.ObjectId == id).FirstOrDefaultAsync();
    }

    public async Task InsertAsync(Api1DeputadoDtoMongo entity)
    {
        var collection = GetEntitiesCollection();
        await collection.InsertOneAsync(entity);
    }
}