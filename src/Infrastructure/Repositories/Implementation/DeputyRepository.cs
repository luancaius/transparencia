using Repositories.DAO;
using Repositories.Interfaces;

namespace Repositories.Implementation;

public class DeputyRepository : IDeputyRepository
{
    public Task<DeputyDao> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<DeputyDao>> ListAsync(int legislatura)
    {
        throw new NotImplementedException();
    }

    public Task<DeputyDao> AddAsync(DeputyDao entity)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(DeputyDao entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(DeputyDao entity)
    {
        throw new NotImplementedException();
    }
}