using Microsoft.EntityFrameworkCore;
using RelationalDatabase.Database;
using RelationalDatabase.Interfaces;

namespace RelationalDatabase.Implementation;

public class SqlServerDatabase : IRelationalDatabase
{
    private readonly DbContext _dbContext;

    public SqlServerDatabase(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void SaveEntity<T>(T entity) where T : class
    {
        var set = _dbContext.Set<T>();
        set.Add(entity);  
        _dbContext.SaveChanges();        
    }
}