using Deputies.Adapter.Out.EFCoreSqlServer.Models;
using Deputies.Application.Ports.Out;
using Deputies.Domain.Entities;
using Deputies.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Deputies.Adapter.Out.EFCoreSqlServer;

public class DeputyRepository : IDeputyRepository
{
    private readonly ILogger<DeputyRepository> _logger;
    private readonly DeputiesDbContext _dbContext;

    public DeputyRepository(
        ILogger<DeputyRepository> logger,
        DeputiesDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task SaveDeputyAsync(Deputy deputy)
    {
        // Check if the person (by CPF) exists
        var cpfValue = deputy.Person.Cpf.GetUnmasked();
        var existingPersonEf = await _dbContext.Persons
            .FirstOrDefaultAsync(p => p.Cpf == cpfValue);

        if (existingPersonEf == null)
        {
            // Create a new EF person
            existingPersonEf = new PersonEfModel
            {
                Cpf = cpfValue,
                FirstName = deputy.Person.Name.FirstName,
                LastName = deputy.Person.Name.LastName,
                FullName = deputy.Person.Name.FullName
            };

            _dbContext.Persons.Add(existingPersonEf);
        }
        else
        {
            // Optionally update if needed
            // existingPersonEf.FirstName = deputy.Person.Name.FirstName;
            // existingPersonEf.LastName  = deputy.Person.Name.LastName;
            // existingPersonEf.FullName  = deputy.Person.Name.FullName;
        }

        // Create EF deputy
        var deputyEf = new DeputyEfModel
        {
            DeputyName = deputy.DeputyName,
            Party = deputy.Party,

            // Serialize the MultiSourceId dictionary to JSON
            SourcesJson = JsonSerializer.Serialize(deputy.MultiSourceId.Ids),

            Person = existingPersonEf
        };

        _dbContext.Deputies.Add(deputyEf);
        await _dbContext.SaveChangesAsync();

        _logger.LogInformation("Saved Deputy {Name} (CPF: {Cpf}) with sources: {Sources}",
            deputy.DeputyName,
            cpfValue,
            deputy.MultiSourceId);
    }

    public async Task<Deputy?> GetDeputyByIdAsync(int deputyId)
    {
        // Include Person to reconstruct the domain
        var deputyEf = await _dbContext.Deputies
            .Include(d => d.Person)
            .FirstOrDefaultAsync(d => d.Id == deputyId);

        if (deputyEf == null)
        {
            _logger.LogWarning("Deputy with Id {Id} not found.", deputyId);
            return null;
        }

        // Deserialize SourcesJson back to a dictionary
        var dict = JsonSerializer.Deserialize<Dictionary<string, string>>(deputyEf.SourcesJson);

        // Rebuild domain's MultiSourceId
        var multiSourceId = MultiSourceId.FromDictionary(dict);

        // Rebuild Person
        var domainPerson = Person.Create(
            new Cpf(deputyEf.Person.Cpf),
            new Name(
                deputyEf.Person.FirstName,
                deputyEf.Person.LastName,
                deputyEf.Person.FullName
            )
        );

        // Rebuild domain Deputy
        var domainDeputy = Deputy.Create(
            domainPerson,
            deputyEf.DeputyName,
            deputyEf.Party,
            multiSourceId
        );

        return domainDeputy;
    }
}