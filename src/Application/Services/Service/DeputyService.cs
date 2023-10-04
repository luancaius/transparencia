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
    
    public async Task<List<Deputy>> GetAllDeputy(int legislatura)
    {
        var deputies = await _deputyRepository.ListAllAsync(legislatura);
        return deputies;
    }

    public Deputy GetDeputy(IQueryable<Deputy> queryable, Expression<Func<Deputy, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public string GetAllDeputyRawRest(int legislatura)
    {
        throw new NotImplementedException();
    }

    public string GetAllDeputyRawSoap(int legislatura)
    {
        throw new NotImplementedException();
    }

    public string GetDeputyRawRest(int legislatura, int id)
    {
        throw new NotImplementedException();
    }

    public string GetDeputyRawSoap(int legislatura, int id)
    {
        throw new NotImplementedException();
    }

    public string GetDeputyExpensesRawRest(int year, int id)
    {
        throw new NotImplementedException();
    }

    public string GetDeputyWorkPresenceRawRest(int year, int id)
    {
        throw new NotImplementedException();
    }
}