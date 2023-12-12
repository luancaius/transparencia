using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RelationalDatabase.Interfaces;

namespace RelationalDatabase.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly DbContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(DbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public T Get(Expression<Func<T, bool>> predicate)
    {
        return _dbSet.Where(predicate).FirstOrDefault();
    }

    public IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate, int page, int pageSize)
    {
        return _dbSet.Where(predicate).Skip(page * pageSize).Take(pageSize).ToList();
    }

    public void Add(T entity)
    {
        _dbSet.Add(entity);
        _context.SaveChanges();
    }

    public void Update(T entity)
    {
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public void Delete(T entity)
    {
        if (_context.Entry(entity).State == EntityState.Detached)
        {
            _dbSet.Attach(entity);
        }
        _dbSet.Remove(entity);
        _context.SaveChanges();
    }
}
