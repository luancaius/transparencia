using Microsoft.EntityFrameworkCore;

namespace Repository.Repositories;

public class EntityFrameworkRepository<T> : IRepository<T> where T : class
{
    private readonly MyDbContext _context;
    
    public EntityFrameworkRepository(MyDbContext context)
    {
        _context = context;
    }
    
    public T GetById(int id)
    {
        return _context.Set<T>().Find(id);
    }
    
    public void Add(T entity)
    {
        _context.Set<T>().Add(entity);
        _context.SaveChanges();
    }
    
    public void Update(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        _context.SaveChanges();
    }
    
    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
        _context.SaveChanges();
    }
}
