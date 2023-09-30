using System.Linq.Expressions;
using Entities.DomainEntities;

namespace Services.Interfaces;

public interface IDeputyService
{
    List<Deputy> GetAllDeputy(IQueryable<Deputy> queryable, Expression<Func<Deputy, bool>> predicate, int page, int pageSize);
    Deputy GetDeputy(IQueryable<Deputy> queryable, Expression<Func<Deputy, bool>> predicate);
    String GetAllDeputyRawRest(int legislatura);
    String GetAllDeputyRawSoap(int legislatura);
    
}