using Deputies.Adapter.Out.EFCoreSqlServer.Models;
using Deputies.Application.Ports.Out;
using Deputies.Domain.Entities;
using Deputies.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Deputies.Adapter.Out.EFCoreSqlServer.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly ILogger<PersonRepository> _logger;
    private readonly DeputiesDbContext _dbContext;

    public PersonRepository(
        ILogger<PersonRepository> logger,
        DeputiesDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task SavePersonAsync(Person person)
    {
        var cpfValue = person.Cpf.GetUnmasked();
        var existingPerson = await _dbContext.Persons
            .FirstOrDefaultAsync(p => p.Cpf == cpfValue);

        if (existingPerson == null)
        {
            existingPerson = new PersonEfModel
            {
                Cpf = cpfValue,
                FirstName = person.PersonName.FirstName,
                LastName = person.PersonName.LastName,
                FullName = person.PersonName.FullName
            };
            _dbContext.Persons.Add(existingPerson);
        }

        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("Person with CPF {Cpf} saved/updated.", cpfValue);
    }

    public async Task<Person?> GetPersonByCpfAsync(string cpf)
    {
        var personEf = await _dbContext.Persons
            .FirstOrDefaultAsync(p => p.Cpf == cpf);

        if (personEf == null)
        {
            _logger.LogWarning("Person with CPF {Cpf} not found.", cpf);
            return null;
        }

        return Person.Create(
            new Cpf(personEf.Cpf),
            new PersonName(
                personEf.FirstName,
                personEf.LastName,
                personEf.FullName
            )
        );
    }
}