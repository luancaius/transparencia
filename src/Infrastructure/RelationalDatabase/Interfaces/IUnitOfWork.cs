using RelationalDatabase.DTO;
using RelationalDatabase.DTO.Deputado;
using RelationalDatabase.Repositories;

namespace RelationalDatabase.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IRepository<Deputado> DeputyRepository { get; }
    IRepository<Supplier> SupplierRepository { get; }
    IRepository<DeputyExpense> DeputyExpenseRepository { get; }
    void SaveChanges();
    Task SaveChangesAsync();
}
