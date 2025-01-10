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
                FirstName = deputy.Person.PersonName.FirstName,
                LastName = deputy.Person.PersonName.LastName,
                FullName = deputy.Person.PersonName.FullName
            };

            _dbContext.Persons.Add(existingPersonEf);
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
            new PersonName(
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

    public async Task<List<Deputy>> GetDeputiesAsync()
    {
        var deputyEf = await _dbContext.Deputies
            .Include(d => d.Person).ToListAsync();
        var deputies = deputyEf.Select(d =>
        {
            var dict = JsonSerializer.Deserialize<Dictionary<string, string>>(d.SourcesJson);

            var multiSourceId = MultiSourceId.FromDictionary(dict);

            var domainPerson = Person.Create(
                new Cpf(d.Person.Cpf),
                new PersonName(
                    d.Person.FirstName,
                    d.Person.LastName,
                    d.Person.FullName
                )
            );

            return Deputy.Create(
                domainPerson,
                d.DeputyName,
                d.Party,
                multiSourceId
            );
        }).ToList();

        return deputies;
    }

    public async Task SaveExpensesAsync(string deputyId, List<Expense> expenses)
    {
        // 1. Check if the deputy exists and get it
        var allDeputies = await _dbContext.Deputies
            .Include(d => d.Person) // includes the Person for the deputy
            .ToListAsync();

        var deputyEf = allDeputies.FirstOrDefault(d =>
        {
            var dict = JsonSerializer.Deserialize<Dictionary<string, string>>(d.SourcesJson);
            return dict?.GetValueOrDefault("CamaraApi") == deputyId;
        });

        if (deputyEf == null)
        {
            _logger.LogWarning("Deputy with external ID {DeputyId} not found in the database.", deputyId);
            return; // or throw an exception
        }

        // 2. Check if expenses already exist for the deputy
        //    We'll load them into memory to do duplicate checks.
        var existingExpenses = await _dbContext.DeputyExpenses
            .Where(e => e.DeputyId == deputyEf.Id)
            .ToListAsync();

        // 3. Continue the flow for the expenses that don't exist.
        //    Define your own logic to decide what makes an expense "already exist."
        //    Here, we assume an expense is the same if (Date, Amount, Description) match.
        var newExpenses = new List<Expense>();

        foreach (var expense in expenses)
        {
            bool alreadyExists = existingExpenses.Any(e =>
                e.Date == expense.Date &&
                e.Amount == expense.Amount &&
                e.Description == expense.Description
            );

            if (!alreadyExists)
            {
                newExpenses.Add(expense);
            }
        }

        if (!newExpenses.Any())
        {
            _logger.LogInformation(
                "No new expenses to save for Deputy (dbId={DeputyDbId}, externalId={ExtId}).",
                deputyEf.Id,
                deputyId
            );
            return;
        }

        // 4. For each new expense:
        //    - The buyer is the deputy's person
        //    - Check if the supplier exists; create if not
        //    - Create the EF expense and save
        foreach (var domainExpense in newExpenses)
        {
            var buyerPerson = deputyEf.Person;

            if (domainExpense.Supplier.GetType() == typeof(Company))
            {
                var supplier = domainExpense.Supplier as Company;
                var supplierCnpj = supplier.Cnpj.GetUnmasked();
                var existingSupplier = await _dbContext.Companies
                    .FirstOrDefaultAsync(c => c.Cnpj == supplierCnpj);

                if (existingSupplier == null)
                {
                    // 4B. If the supplier does not exist, create it
                    existingSupplier = new CompanyEfModel
                    {
                        Cnpj = supplierCnpj,
                        CompanyName = supplier.CompanyName
                    };
                    _dbContext.Companies.Add(existingSupplier);

                    await _dbContext.SaveChangesAsync();
                }
            }
            else
            {
                // 4A. If the supplier is a Person, check if it exists

                var supplier = domainExpense.Supplier as Person;
                var supplierCpf = supplier.Cpf.GetUnmasked();
                var existingSupplier = await _dbContext.Persons
                    .FirstOrDefaultAsync(p => p.Cpf == supplierCpf);

                if (existingSupplier == null)
                {
                    existingSupplier = new PersonEfModel
                    {
                        Cpf = supplierCpf,
                        FullName = supplierCpf // or a real name, if available
                    };
                    _dbContext.Persons.Add(existingSupplier);

                    await _dbContext.SaveChangesAsync();
                }

                // 4D. Create the EF expense
                var expenseEf = new DeputyExpenseEfModel
                {
                    DeputyId = deputyEf.Id,
                    BuyerPersonId = buyerPerson.Id,
                    SupplierPersonId = existingSupplier.Id,
                    Amount = domainExpense.Amount,
                    Description = domainExpense.Description,
                    Date = domainExpense.Date
                };

                // 4E. Save the EF expense
                _dbContext.DeputyExpenses.Add(expenseEf);
            }

            await _dbContext.SaveChangesAsync();

            _logger.LogInformation(
                "Saved {Count} new expenses for Deputy (dbId={DeputyDbId}, externalId={ExtId}).",
                newExpenses.Count,
                deputyEf.Id,
                deputyId
            );
        }
    }
}