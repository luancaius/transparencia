using MongoDB.Bson;
using MongoDB.Driver;

namespace Repository.Repositories.Mongo;

public interface IMongoRepository<T>
{
    IMongoCollection<T> GetEntitiesCollection();

    Task<T> GetByIdAsync(ObjectId id);

    Task InsertAsync(T entity);
    
    Task InsertManyAsync(List<T> entity);

    Task<List<T>> GetAll();
}