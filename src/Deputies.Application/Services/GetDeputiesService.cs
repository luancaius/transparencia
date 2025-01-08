using Deputies.Application.Ports.In;
using Deputies.Application.Ports.Out;
using Deputies.Domain.Entities;
using Deputies.Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace Deputies.Application.Services;

public class GetDeputiesService : IGetDeputiesUseCase
{
    private readonly IDeputyProvider _deputyProvider;
    private readonly IDeputyRepository _deputyRepository;
    private readonly ILogger<GetDeputiesService> _logger;

    public GetDeputiesService(
        IDeputyProvider deputyProvider,
        IDeputyRepository deputyRepository,
        ILogger<GetDeputiesService> logger)
    {
        _deputyProvider = deputyProvider;
        _deputyRepository = deputyRepository;
        _logger = logger;
    }

    public async Task ProcessDeputiesAsync(int year)
    {
        var legislatura = CalculateLegislatura(year);
        var deputiesList = await _deputyProvider.GetDeputiesListAsync(legislatura);

        foreach (var deputyListItem in deputiesList)
        {
            try
            {
                var details = await _deputyProvider.GetDeputyDetailAsync(deputyListItem.Id);

                var name = new PersonName(null, null, details.NomeCivil);
                var cpf = new Cpf(details.Cpf);
                    
                var person = Person.Create(cpf, name);

                var multiSourceId = new MultiSourceId("CamaraApi", details.Id.ToString());
                    
                var deputy = Deputy.Create(
                    person,
                    details.Nome,
                    details.SiglaPartido,
                    multiSourceId
                );

                await _deputyRepository.SaveDeputyAsync(deputy);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing deputy {Id}", deputyListItem.Id);
            }
        }
    }

    public Task ProcessDeputiesExpensesAsync(int year)
    {
        throw new NotImplementedException();
    }

    public async Task ProcessDeputiesExpensesCurrentMonthAsync()
    {
        var deputiesList = await _deputyRepository.GetDeputiesAsync();

        foreach (var deputy in deputiesList)
        {
            var deputyId = deputy.MultiSourceId.Ids.GetValueOrDefault("CamaraApi");
            if (string.IsNullOrWhiteSpace(deputyId))
                continue;

            var expensesDtos = await _deputyProvider.GetDeputyExpensesAsync(
                deputyId,
                DateTime.Now.Year,
                DateTime.Now.Month
            );

            var buyer = deputy.Person;
            var domainExpenses = new List<Expense>();

            foreach (var dto in expensesDtos)
            {
                var supplier = Company.Create(new Cnpj(dto.CnpjCpfFornecedor), dto.NomeFornecedor);
                var domainExpense = new Expense(
                    amount: dto.ValorDocumento,
                    date: dto.DataDocumento,
                    description: dto.TipoDespesa,
                    buyer: buyer,
                    supplier: supplier
                );

                domainExpenses.Add(domainExpense);
            }
            await _deputyRepository.SaveExpensesAsync(deputyId, domainExpenses);
        }
    }

    private int CalculateLegislatura(int year)
    {
        int baseYear = 2023;
        int baseLegislatura = 57;
        int yearDiff = year - baseYear;
        return baseLegislatura + (yearDiff / 4);
    }
}