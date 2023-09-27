using System.Linq.Expressions;
using Entities.DomainEntities;

namespace Services.Interfaces;

public interface IPersonService
{
    Person GetPerson(IQueryable<Person> queryable, Expression<Func<Person, bool>> predicate);
    List<Person> GetPeople(IQueryable<Person> queryable, Expression<Func<Person, bool>> predicate, int page, int pageSize);
}