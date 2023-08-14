using MongoDB.Bson;
using MongoDB.Driver;
using Service.DTO.API1;

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
    
    public IMongoCollection<Api1DeputadoDto> GetEntitiesCollection()
    {
        return _database.GetCollection<Api1DeputadoDto>($"temp_api1_deputados");
    }

    public async Task<Api1DeputadoDto> GetByIdAsync(ObjectId id)
    {
        var collection = GetEntitiesCollection();
        return await collection.Find(e => e.Id == id).FirstOrDefaultAsync();
    }

    public async Task InsertAsync(Api1DeputadoDto entity)
    {
        var collection = GetEntitiesCollection();
        await collection.InsertOneAsync(entity);
    }
}