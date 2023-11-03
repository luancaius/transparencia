namespace NonRelationalDatabase.Interfaces;

public interface INonRelationalDatabase
{   
    Task Insert<T>(T entity);
    Task<T> Get<T>(string id);
    Task<IEnumerable<T>> GetAll<T>();
}