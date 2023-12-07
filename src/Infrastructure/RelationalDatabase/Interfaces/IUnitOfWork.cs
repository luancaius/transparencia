using RelationalDatabase.DTO.Deputado;
using RelationalDatabase.Repositories;

namespace Repositories.Interfaces;

public interface IUnitOfWork : IDisposable
{
    // Interface for each repository the unit of work will handle
    IRepository<Deputado> DeputyRepository { get; }

    // Method to save all changes
    void SaveChanges();
    Task SaveChangesAsync();
}
