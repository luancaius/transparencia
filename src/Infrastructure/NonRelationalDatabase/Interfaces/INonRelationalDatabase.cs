namespace NonRelationalDatabase.Interfaces;

public interface INonRelationalDatabase
{   
    Task Insert<T>(T entity);
    
    Task InsertMany<T>(IEnumerable<T> entities);
    
    Task<T> Get<T>(string id);
    
    Task<IEnumerable<T>> GetAll<T>();
}