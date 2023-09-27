using System.Linq.Expressions;
using Entities.DomainEntities;
using Services.Interfaces;

namespace Services.Service;

public class PersonService : IPersonService
{
    public Person GetPerson(IQueryable<Person> queryable, Expression<Func<Person, bool>> predicate)
    {
        return queryable.Where(predicate).FirstOrDefault();
    }

    public List<Person> GetPeople(IQueryable<Person> queryable, Expression<Func<Person, bool>> predicate, int page, int pageSize)
    {
        return queryable.Where(predicate).Skip(page * pageSize).Take(pageSize).ToList();
    }
}
