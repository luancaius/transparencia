using MongoDB.Bson;
using MongoDB.Driver;
using Repository.JsonEntity;

namespace Repository.Repositories.Mongo;

public class Api1DeputadoMongoRepository : IMongoRepository<Api1DeputadoDtoMongo>
{
    private readonly IMongoDatabase _database;
    private string _tableName;

    public Api1DeputadoMongoRepository(MongoDbContext mongoContext, string tableName)
    {
        _database = mongoContext.Database;
        _tableName = tableName;

        Util.createUniqueIndexForTable<Api1DeputadoDtoMongo>(_database, tableName, "Nome");
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

    public Task InsertManyAsync(List<Api1DeputadoDtoMongo> entity)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Api1DeputadoDtoMongo>> GetAll()
    {
        var collection = GetEntitiesCollection();
        var filter = Builders<Api1DeputadoDtoMongo>.Filter.Empty; 
        return await collection.Find(filter).ToListAsync();
    }
}