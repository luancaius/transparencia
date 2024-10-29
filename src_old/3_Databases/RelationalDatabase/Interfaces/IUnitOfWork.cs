using RelationalDatabase.DTO;
using RelationalDatabase.DTO.Deputado;

namespace RelationalDatabase.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IRepository<Deputado> DeputyRepository { get; }
    IRepository<Supplier> SupplierRepository { get; }
    IRepository<DeputyExpense> DeputyExpenseRepository { get; }
    IRepository<Company> CompanyRepository { get; }
    IRepository<Person> PersonRepository { get; }

    void SaveChanges();
    Task SaveChangesAsync();
}
