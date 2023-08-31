using MongoDB.Bson;
using MongoDB.Driver;
using Repository.JsonEntity;

namespace Repository;

public class Api1MongoRepository : IMongoRepository<Api1DeputadoDtoMongo>
{
    private readonly IMongoDatabase _database;
    private string _tableName;

    public Api1MongoRepository(MongoDbContext mongoContext, string tableName)
    {
        _database = mongoContext.Database;
        _tableName = tableName;
        
        var indexKeys = Builders<Api1DeputadoDtoMongo>.IndexKeys.Ascending("Nome");
        var indexOptions = new CreateIndexOptions { Unique = true };
        var indexModel = new CreateIndexModel<Api1DeputadoDtoMongo>(indexKeys, indexOptions);
        var collection = _database.GetCollection<Api1DeputadoDtoMongo>(tableName);
        collection.Indexes.CreateOne(indexModel); // Create the unique index
    }
    
    public IMongoCollection<Api1DeputadoDtoMongo> GetEntitiesCollection()
    {
        return _database.GetCollection<Api1DeputadoDtoMongo>(_tableName);
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

    public async Task<List<Api1DeputadoDtoMongo>> GetAll()
    {
        var collection = GetEntitiesCollection();
        var filter = Builders<Api1DeputadoDtoMongo>.Filter.Empty; 
        return await collection.Find(filter).ToListAsync();
    }
}