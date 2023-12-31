using System.Linq.Expressions;

namespace RelationalDatabase.Interfaces;

public interface IRepository<T> where T : class
{
    T? Get(Expression<Func<T, bool>> predicate);
    IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate, int page, int pageSize);
    void Add(T entity);
    void UpdateInsert(T entity, Expression<Func<T, bool>> filter);
    void Update(T entity);
    void Delete(T entity);
}