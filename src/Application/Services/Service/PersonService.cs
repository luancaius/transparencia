using System.Linq.Expressions;
using Entities.DomainEntities;
using RelationalDatabase.Repositories;
using Services.Interfaces;

namespace Services.Service;

public class PersonService : IPersonService
{
    private readonly IRepository<Person> _personRepository;

    public PersonService(IRepository<Person> personRepository)
    {
        _personRepository = personRepository;
    }

    public Person GetPerson(IQueryable<Person> queryable, Expression<Func<Person, bool>> predicate)
    {
        return _personRepository.Get(predicate);
    }

    public List<Person> GetPeople(IQueryable<Person> queryable, Expression<Func<Person, bool>> predicate, int page, int pageSize)
    {
        return _personRepository.GetAll(predicate, page, pageSize).ToList();
    }

    public int InsertPerson(Person person)
    {
        throw new NotImplementedException();
    }
}

