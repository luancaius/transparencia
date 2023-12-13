using RelationalDatabase.DTO.Deputado;
using RelationalDatabase.Repositories;

namespace RelationalDatabase.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IRepository<Deputado> DeputyRepository { get; }
    void SaveChanges();
    Task SaveChangesAsync();
}
