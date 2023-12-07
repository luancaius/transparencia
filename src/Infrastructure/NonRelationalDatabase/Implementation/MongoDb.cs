using MongoDB.Driver;
using NonRelationalDatabase.Helpers;
using NonRelationalDatabase.Interfaces;
using Repositories.DTO;
using Serilog;

namespace NonRelationalDatabase.Implementation;

public class MongoDb : INonRelationalDatabase
{
    private readonly MongoDbHelper _mongoDbHelper;
    private readonly ILogger _logger;

    public MongoDb(MongoDbHelper mongoDbHelper, ILogger logger)
    {
        _mongoDbHelper = mongoDbHelper;
        _logger = logger.ForContext<MongoDb>();
    }

    public Task Insert<T>(T entity)
    {
        string collectionName = typeof(T).Name;
        _mongoDbHelper.InsertData(collectionName, entity);
        return Task.CompletedTask;
    }

    public Task CheckAndUpdate<T>(T entity) where T : BaseEntityDTO
    {
        var id = entity.Id;
        _logger.Information($"CheckAndUpdate {entity.GetType()} id: {id}");

        string collectionName = typeof(T).Name;
        FilterDefinition<T> filter = Builders<T>.Filter.Eq("Id", id); // Assuming the entity has an Id property
        var originalEntity = _mongoDbHelper.GetData(collectionName, filter).FirstOrDefault();
        if (originalEntity == null)
        {
            _mongoDbHelper.InsertData(collectionName, entity);
            return Task.CompletedTask;
        }
        
        _mongoDbHelper.DeleteData(collectionName, filter);
        originalEntity = _mongoDbHelper.GetData(collectionName, filter).FirstOrDefault();
        if (originalEntity == null)
        {
            _mongoDbHelper.InsertData(collectionName, entity);
        }
        else
        {
            _logger.Error($"Some error when checkandupdate for id:{id} entity:{entity.GetType()}");   
        }
        return Task.CompletedTask;
    }

    public async Task<T> Get<T>(string id)
    {
        string collectionName = typeof(T).Name;
        FilterDefinition<T> filter = Builders<T>.Filter.Eq("Id", id); // Assuming the entity has an Id property
        var result = _mongoDbHelper.GetData(collectionName, filter);
        return result.FirstOrDefault();
    }

    public async Task<IEnumerable<T>> GetAll<T>(int? legislatura)
    {
        string collectionName = typeof(T).Name;
        FilterDefinition<T> filter = null;
        if(legislatura.HasValue)
            filter = Builders<T>.Filter.Eq("IdLegislatura", legislatura); 
        var result = _mongoDbHelper.GetData<T>(collectionName, filter);
        return result;
    }   
}