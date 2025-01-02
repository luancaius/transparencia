using Deputies.Adapter.Out.EFCoreSqlServer.Models;
using Deputies.Domain.Entities;
using Deputies.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Deputies.Adapter.Out.EFCoreSqlServer;

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
                FirstName = person.Name.FirstName,
                LastName = person.Name.LastName,
                FullName = person.Name.FullName
            };
            _dbContext.Persons.Add(existingPerson);
        }
        else
        {
            // Optionally update
            // existingPerson.FirstName = person.Name.FirstName;
            // existingPerson.LastName = person.Name.LastName;
            // existingPerson.FullName = person.Name.FullName;
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
            new Name(
                personEf.FirstName,
                personEf.LastName,
                personEf.FullName
            )
        );
    }
}