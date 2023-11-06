using MongoDB.Driver;
using NonRelationalDatabase.Helpers;
using NonRelationalDatabase.Interfaces;

namespace NonRelationalDatabase.Implementation;

public class MongoDb : INonRelationalDatabase
{
    private readonly MongoDbHelper _mongoDbHelper;

    public MongoDb(MongoDbHelper mongoDbHelper)
    {
        _mongoDbHelper = mongoDbHelper;
    }

    public Task Insert<T>(T entity)
    {
        string collectionName = typeof(T).Name;
        _mongoDbHelper.InsertData(collectionName, entity);
        return Task.CompletedTask;
    }

    public Task CheckAndUpdate<T>(T entity, string id)
    {
        string collectionName = typeof(T).Name;
        FilterDefinition<T> filter = Builders<T>.Filter.Eq("IdEntity", id); // Assuming the entity has an Id property
        var originalEntity = _mongoDbHelper.GetData(collectionName, filter).FirstOrDefault();
        if (originalEntity != null)
        {
            _mongoDbHelper.DeleteData(collectionName, filter);
        }
        originalEntity = _mongoDbHelper.GetData(collectionName, filter).FirstOrDefault();
        if (originalEntity == null)
        {
            _mongoDbHelper.InsertData(collectionName, entity);
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

    public async Task<IEnumerable<T>> GetAll<T>()
    {
        string collectionName = typeof(T).Name;
        var result = _mongoDbHelper.GetData<T>(collectionName);
        return result;
    }
}