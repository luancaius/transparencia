using System.Linq.Expressions;

namespace NonRelationalDatabase.Interfaces;

public interface INonRelationalDatabase
{   
    Task Upsert<T>(T entity);
    Task<T?> Get<T>(string id);
    Task<List<T>> GetAll<T>(Expression<Func<T, bool>>? filterExpression = null);
}