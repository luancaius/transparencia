using MongoDB.Bson;
using MongoDB.Driver;
using Repository.JsonEntity;

namespace Repository;

public class Api2MongoRepository : IMongoRepository<Api2DeputadoDtoMongo>
{
    private readonly IMongoClient _mongoClient;
    private readonly IMongoDatabase _database;
    private string _tableName;

    public Api2MongoRepository(MongoDbContext mongoContext, string tableName)
    {
        _database = mongoContext.Database;
        _tableName = tableName;
        
        var indexKeys = Builders<Api1DeputadoDtoMongo>.IndexKeys.Ascending("Nome");
        var indexOptions = new CreateIndexOptions { Unique = true };
        var indexModel = new CreateIndexModel<Api1DeputadoDtoMongo>(indexKeys, indexOptions);
        var collection = _database.GetCollection<Api1DeputadoDtoMongo>(tableName);
        collection.Indexes.CreateOne(indexModel); // Create the unique index
    }
    
    public IMongoCollection<Api2DeputadoDtoMongo> GetEntitiesCollection()
    {
        return _database.GetCollection<Api2DeputadoDtoMongo>(_tableName);
    }

    public async Task<Api2DeputadoDtoMongo> GetByIdAsync(ObjectId id)
    {
        var collection = GetEntitiesCollection();
        return await collection.Find(e => e._id == id).FirstOrDefaultAsync();
    }

    public async Task InsertAsync(Api2DeputadoDtoMongo entity)
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

    public Task<List<Api2DeputadoDtoMongo>> GetAll()
    {
        throw new NotImplementedException();
    }
}