using System.Text.Json;
using Deputies.Adapter.Out.EFCoreSqlServer.Models;
using Deputies.Application.Ports.Out;
using Deputies.Domain.AbstractEntities;
using Deputies.Domain.Entities;
using Deputies.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Deputies.Adapter.Out.EFCoreSqlServer.Repositories;

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
        var cpfValue = deputy.Cpf.GetUnmasked();
        var existingPersonEf = await _dbContext.Persons
            .FirstOrDefaultAsync(p => p.Cpf == cpfValue);

        if (existingPersonEf == null)
        {
            // Create a new EF person
            existingPersonEf = new PersonEfModel
            {
                Cpf = cpfValue,
                FirstName = deputy.PersonName.FirstName,
                LastName = deputy.PersonName.LastName,
                FullName = deputy.PersonName.FullName
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
            new Cpf(deputyEf.Person.Cpf),
            new PersonName(
                deputyEf.Person.FirstName,
                deputyEf.Person.LastName,
                deputyEf.Person.FullName
            ),
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

            return Deputy.Create(
                new Cpf(d.Person.Cpf),
                new PersonName(
                    d.Person.FirstName,
                    d.Person.LastName,
                    d.Person.FullName
                ),
                d.DeputyName,
                d.Party,
                multiSourceId
            );
        }).ToList();

        return deputies;
    }

    public async Task SaveExpensesAsync(string deputyId, List<Expense> expenses)
    {
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

        var existingExpenses = await _dbContext.DeputyExpenses
            .Where(e => e.DeputyId == deputyEf.Id)
            .ToListAsync();

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

        foreach (var domainExpense in newExpenses)
        {
            var buyerPerson = deputyEf.Person;
            var supplierCpfCnpj = string.Empty;
            if (domainExpense.Supplier.GetType() == typeof(Company))
            {
                var supplier = domainExpense.Supplier as Company;
                var supplierCnpj = supplier.Cnpj.GetUnmasked();
                var existingSupplier = await _dbContext.Companies
                    .FirstOrDefaultAsync(c => c.Cnpj == supplierCnpj);

                if (existingSupplier == null)
                {
                    existingSupplier = new CompanyEfModel
                    {
                        Cnpj = supplierCnpj,
                        CompanyName = supplier.CompanyName
                    };
                    _dbContext.Companies.Add(existingSupplier);

                    await _dbContext.SaveChangesAsync();
                }

                supplierCpfCnpj = supplierCnpj;
            }
            else
            {
                var supplier = domainExpense.Supplier as Person;
                var supplierCpf = supplier.Cpf.GetUnmasked();
                var existingSupplier = await _dbContext.Persons
                    .FirstOrDefaultAsync(p => p.Cpf == supplierCpf);

                if (existingSupplier == null)
                {
                    existingSupplier = new PersonEfModel
                    {
                        Cpf = supplierCpf,
                        FullName = supplierCpf
                    };
                    _dbContext.Persons.Add(existingSupplier);

                    await _dbContext.SaveChangesAsync();
                }

                supplierCpfCnpj = supplierCpf;
            }

            var expenseEf = new DeputyExpenseEfModel
            {
                DeputyId = deputyEf.Id,
                BuyerPersonId = buyerPerson.Id,
                SupplierCpfCnpj = supplierCpfCnpj,
                Amount = domainExpense.Amount,
                Description = domainExpense.Description,
                Date = domainExpense.Date,
                UrlDocument = domainExpense.UrlDocument
            };

            _dbContext.DeputyExpenses.Add(expenseEf);

            await _dbContext.SaveChangesAsync();

            _logger.LogInformation(
                "Saved {Count} new expenses for Deputy (dbId={DeputyDbId}, externalId={ExtId}).",
                newExpenses.Count,
                deputyEf.Id,
                deputyId
            );
        }
    }

    public async Task<List<Expense>> GetExpensesByYearAndMonthAsync(int year, int month, int top = 10)
    {
        var expenseEfList = await _dbContext.DeputyExpenses
            .Include(e => e.Buyer) // If you still need it for some reason, but not strictly necessary
            .Include(e => e.Deputy)
            .ThenInclude(d => d.Person)
            .Where(e => e.Date.Year == year && e.Date.Month == month)
            .OrderByDescending(e => e.Amount)
            .Take(top)
            .ToListAsync();

        var domainExpenses = new List<Expense>();

        foreach (var expenseEf in expenseEfList)
        {
            Deputy? domainBuyer = null;
            if (expenseEf.Deputy != null && expenseEf.Deputy.Person != null)
            {
                var dict = JsonSerializer.Deserialize<Dictionary<string, string>>(expenseEf.Deputy.SourcesJson);
                var multiSourceId = MultiSourceId.FromDictionary(dict);

                domainBuyer = Deputy.Create(
                    new Cpf(expenseEf.Deputy.Person.Cpf),
                    new PersonName(
                        expenseEf.Deputy.Person.FirstName,
                        expenseEf.Deputy.Person.LastName,
                        expenseEf.Deputy.Person.FullName
                    ),
                    expenseEf.Deputy.DeputyName,
                    expenseEf.Deputy.Party,
                    multiSourceId
                );
            }
            else
            {
                _logger.LogWarning("Expense {ExpenseId} does not have an associated Deputy or Person.", expenseEf.Id);
                continue;
            }

            var supplierCpfCnpj = expenseEf.SupplierCpfCnpj;

            Participant domainSupplier = null;
            var supplierPersonEf = await _dbContext.Persons
                .FirstOrDefaultAsync(p => p.Cpf == supplierCpfCnpj);

            if (supplierPersonEf != null)
            {
                domainSupplier = Person.Create(
                    new Cpf(supplierPersonEf.Cpf),
                    new PersonName(
                        supplierPersonEf.FirstName,
                        supplierPersonEf.LastName,
                        supplierPersonEf.FullName
                    )
                );
            }
            else
            {
                var supplierCompanyEf = await _dbContext.Companies
                    .FirstOrDefaultAsync(c => c.Cnpj == supplierCpfCnpj);

                if (supplierCompanyEf != null)
                {
                    domainSupplier = Company.Create(
                        new Cnpj(supplierCompanyEf.Cnpj),
                        supplierCompanyEf.CompanyName
                    );
                }
                else
                {
                    _logger.LogWarning(
                        "Could not find supplier {Supplier} in Persons or Companies for Expense {ExpenseId}",
                        supplierCpfCnpj,
                        expenseEf.Id
                    );
                }
            }

            if (domainSupplier != null && domainBuyer != null)
            {
                domainExpenses.Add(
                    new Expense(
                        expenseEf.Amount,
                        expenseEf.Date,
                        expenseEf.Description,
                        domainBuyer,
                        domainSupplier,
                        expenseEf.UrlDocument
                    )
                );
            }
        }

        return domainExpenses;
    }
}