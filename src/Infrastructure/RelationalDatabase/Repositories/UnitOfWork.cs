using RelationalDatabase.Database;
using RelationalDatabase.DTO.Deputado;
using RelationalDatabase.Interfaces;

namespace RelationalDatabase.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private IRepository<Deputado> _deputyRepository;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }
    
    public IRepository<Deputado> DeputyRepository
    {
        get
        {
            return _deputyRepository ?? (_deputyRepository = new Repository<Deputado>(_context));
        }
    }   
    
    public void SaveChanges()
    {
        _context.SaveChanges();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            // Dispose managed resources.
            _context?.Dispose();
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}