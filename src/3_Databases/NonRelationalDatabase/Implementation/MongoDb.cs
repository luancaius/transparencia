using System.Linq.Expressions;
using NonRelationalDatabase.Helpers;
using NonRelationalDatabase.Interfaces;
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

    public Task Update<T>(T entity)
    {
        throw new NotImplementedException();
    }
    
    public Task<T?> Get<T>(string id)
    {
        throw new NotImplementedException();
    }

    public Task<List<T>> GetAll<T>(Expression<Func<T, bool>>? filterExpression = null)
    {
        throw new NotImplementedException();
    }
}

//
//     public Task Insert<T>(T entity)
//     {
//         string collectionName = typeof(T).Name;
//         _mongoDbHelper.InsertData(collectionName, entity);
//         return Task.CompletedTask;
//     }
//
//     public Task CheckAndUpdate<T>(T entity)
//     {
//         var id = entity.Id;
//         _logger.Information($"CheckAndUpdate {entity.GetType()} id: {id}");
//
//         string collectionName = typeof(T).Name;
//         FilterDefinition<T>? filter = Builders<T>.Filter.Eq("Id", id); // Assuming the entity has an Id property
//         var originalEntity = _mongoDbHelper.GetData(collectionName, filter).FirstOrDefault();
//         if (originalEntity == null)
//         {
//             _mongoDbHelper.InsertData(collectionName, entity);
//             return Task.CompletedTask;
//         }
//         
//         _mongoDbHelper.DeleteData(collectionName, filter);
//         originalEntity = _mongoDbHelper.GetData(collectionName, filter).FirstOrDefault();
//         if (originalEntity == null)
//         {
//             _mongoDbHelper.InsertData(collectionName, entity);
//         }
//         else
//         {
//             _logger.Error($"Some error when checkandupdate for id:{id} entity:{entity.GetType()}");   
//         }
//         return Task.CompletedTask;
//     }
//
//     public Task<T?> Get<T>(string id)
//     {
//         string collectionName = typeof(T).Name;
//         FilterDefinition<T>? filter = Builders<T>.Filter.Eq("Id", id); // Assuming the entity has an Id property
//         var result = _mongoDbHelper.GetData(collectionName, filter);
//         return Task.FromResult(result.FirstOrDefault());
//     }
//
//     public Task<List<T>> GetAll<T>(Expression<Func<T, bool>>? filterExpression = null)
//     {
//         string collectionName = typeof(T).Name;
//         FilterDefinition<T>? filter = filterExpression != null ? 
//             Builders<T>.Filter.Where(filterExpression) : 
//             Builders<T>.Filter.Empty;
//         var result = _mongoDbHelper.GetData<T>(collectionName, filter);
//         return Task.FromResult(result);
//     }   
// }