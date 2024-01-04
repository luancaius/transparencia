using RelationalDatabase.Database;
using RelationalDatabase.DTO;
using RelationalDatabase.DTO.Deputado;
using RelationalDatabase.Interfaces;

namespace RelationalDatabase.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private IRepository<Deputado> _deputyRepository;
    private IRepository<Supplier> _supplierRepository;
    private IRepository<DeputyExpense> _deputyExpenseRepository;

    public IRepository<Deputado> DeputyRepository
    {
        get { return _deputyRepository ??= new Repository<Deputado>(_context); }
    }

    public IRepository<Supplier> SupplierRepository
    {
        get { return _supplierRepository ??= new Repository<Supplier>(_context); }
    }

    public IRepository<DeputyExpense> DeputyExpenseRepository
    {
        get { return _deputyExpenseRepository ??= new Repository<DeputyExpense>(_context); }
    }

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
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
            _context?.Dispose();
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~UnitOfWork()  // Finalizer
    {
        Dispose(false);
    }
}