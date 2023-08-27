using MongoDB.Bson;
using MongoDB.Driver;

namespace Repository;

public interface IMongoRepository<T>
{
    IMongoCollection<T> GetEntitiesCollection();

    Task<T> GetByIdAsync(ObjectId id);

    Task InsertAsync(T entity);
}