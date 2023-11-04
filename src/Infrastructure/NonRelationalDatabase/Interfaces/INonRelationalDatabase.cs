using MongoDB.Driver;

namespace NonRelationalDatabase.Interfaces;

public interface INonRelationalDatabase
{   
    Task Insert<T>(T entity);
    Task CheckAndUpdate<T>(T entity, string Id);
    Task<T> Get<T>(string id);
    Task<IEnumerable<T>> GetAll<T>();
}