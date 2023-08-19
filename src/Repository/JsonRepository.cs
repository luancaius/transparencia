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
        
        var indexKeys = Builders<Api1DeputadoDtoMongo>.IndexKeys.Ascending("Nome");
        var indexOptions = new CreateIndexOptions { Unique = true };
        var indexModel = new CreateIndexModel<Api1DeputadoDtoMongo>(indexKeys, indexOptions);
        var collection = _database.GetCollection<Api1DeputadoDtoMongo>($"api1_deputados");
        collection.Indexes.CreateOne(indexModel); // Create the unique index

    }
    
    public IMongoCollection<Api1DeputadoDtoMongo> GetEntitiesCollection()
    {
        return _database.GetCollection<Api1DeputadoDtoMongo>($"api1_deputados");
    }

    public async Task<Api1DeputadoDtoMongo> GetByIdAsync(ObjectId id)
    {
        var collection = GetEntitiesCollection();
        return await collection.Find(e => e._id == id).FirstOrDefaultAsync();
    }

    public async Task InsertAsync(Api1DeputadoDtoMongo entity)
    {
        var collection = GetEntitiesCollection();
        try
        {
            await collection.InsertOneAsync(entity);
        }
        catch (Exception e)
        {
            Console.WriteLine("JsonRepository - InsertAsync error:"+e.Message);
        }
    }
}