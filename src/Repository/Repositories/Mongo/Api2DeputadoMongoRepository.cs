using MongoDB.Bson;
using MongoDB.Driver;
using Repository.JsonEntity;

namespace Repository.Repositories.Mongo;

public class Api2DeputadoMongoRepository : IMongoRepository<Api2DeputadoDtoMongo>
{
    private readonly IMongoDatabase _database;
    private string _tableName;

    public Api2DeputadoMongoRepository(MongoDbContext mongoContext, string tableName)
    {
        _database = mongoContext.Database;
        _tableName = tableName;

        Util.createUniqueIndexForTable<Api2DeputadoDtoMongo>(_database, tableName, "Nome");

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

    public Task InsertManyAsync(List<Api2DeputadoDtoMongo> entity)
    {
        throw new NotImplementedException();
    }

    public Task<List<Api2DeputadoDtoMongo>> GetAll()
    {
        throw new NotImplementedException();
    }
}