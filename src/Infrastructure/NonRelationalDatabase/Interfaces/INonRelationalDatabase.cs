using Repositories.DTO;

namespace NonRelationalDatabase.Interfaces;

public interface INonRelationalDatabase
{   
    Task Insert<T>(T entity);
    Task CheckAndUpdate<T>(T entity) where T : BaseEntityDTO;
    Task<T> Get<T>(string id);
    Task<IEnumerable<T>> GetAll<T>();
}