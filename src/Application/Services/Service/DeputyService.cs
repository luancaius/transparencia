using System.Linq.Expressions;
using Entities.DomainEntities;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services.Service;

public class DeputyService : IDeputyService
{
    private readonly IDeputyRepository _deputyRepository;
    private readonly ISearchDeputyRepository _searchDeputyRepository;

    public DeputyService(IDeputyRepository deputyRepository, ISearchDeputyRepository searchDeputyRepository)
    {
        _deputyRepository = deputyRepository;
        _searchDeputyRepository = searchDeputyRepository;
    }

    public async Task<string> GetAllDeputyRaw(int legislatura)
    {
        var deputiesString = await _searchDeputyRepository.GetAllDeputiesRaw(legislatura);
        return deputiesString;
    }

    public async Task<string> GetDeputyRaw(int legislatura, int id)
    {
        throw new NotImplementedException();
    }

    public async Task<string> GetDeputyExpensesRaw(int year, int id)
    {
        throw new NotImplementedException();
    }

    public async Task<string> GetDeputyWorkPresenceRaw(int year, int id)
    {
        throw new NotImplementedException();
    }
}