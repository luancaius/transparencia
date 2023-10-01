using System.Linq.Expressions;
using Entities.DomainEntities;
using Services.Interfaces;

namespace Services.Service;

public class DeputyService : IDeputyService
{
    public List<Deputy> GetAllDeputy(IQueryable<Deputy> queryable, Expression<Func<Deputy, bool>> predicate, int page, int pageSize)
    {
        throw new NotImplementedException();
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