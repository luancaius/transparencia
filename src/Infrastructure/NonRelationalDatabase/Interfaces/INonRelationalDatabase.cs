using System.Linq.Expressions;
using Repositories.DTO;

namespace NonRelationalDatabase.Interfaces;

public interface INonRelationalDatabase
{   
    Task Insert<T>(T entity);
    Task CheckAndUpdate<T>(T entity) where T : BaseEntityDTO;
    Task<T> Get<T>(string id);
    Task<List<T>> GetAll<T>(Expression<Func<T, bool>>? filterExpression = null);
}