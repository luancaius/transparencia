using System.Linq.Expressions;
using Entities.DomainEntities;

namespace Services.Interfaces;

public interface IDeputyService
{
    List<Deputy> GetAllDeputy(IQueryable<Deputy> queryable, Expression<Func<Deputy, bool>> predicate, int page, int pageSize);
    Deputy GetDeputy(IQueryable<Deputy> queryable, Expression<Func<Deputy, bool>> predicate);
    String GetAllDeputyRawRest(int legislatura);
    String GetAllDeputyRawSoap(int legislatura);
    String GetDeputyRawRest(int legislatura, int id);
    String GetDeputyRawSoap(int legislatura, int id);
    String GetDeputyExpensesRawRest(int year, int id);
    String GetDeputyWorkPresenceRawRest(int year, int id);
}