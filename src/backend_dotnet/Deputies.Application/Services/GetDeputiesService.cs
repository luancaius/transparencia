using Deputies.Application.Dtos;
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
                await CreateOrUpdateDeputyAsync(deputyListItem.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing deputy {Id}", deputyListItem.Id);
            }
        }
    }

    public async Task ProcessDeputiesExpensesByYearAsync(int year)
    {
        try
        {
            var months = (DateTime.Now.Year == year)
                ? Enumerable.Range(1, DateTime.Now.Month)
                : Enumerable.Range(1, 12);

            await ProcessDeputiesExpensesForMonthsAsync(year, months);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing deputies expenses for year {year}", year);
        }
    }

    public async Task ProcessDeputiesExpensesCurrentMonthAsync()
    {
        await ProcessDeputiesExpensesForMonthsAsync(DateTime.Now.Year, new[] { DateTime.Now.Month });
    }

    public async Task ProcessDeputiesExpensesByMonthAndYearAsync(int year, int month)
    {
        await ProcessDeputiesExpensesForMonthsAsync(year, new[] { month });
    }

    private async Task CreateOrUpdateDeputyAsync(int deputyId)
    {
        var details = await _deputyProvider.GetDeputyDetailAsync(deputyId);
        var name = new PersonName(null, null, details.NomeCivil);
        var cpf = new Cpf(details.Cpf);
        var multiSourceId = new MultiSourceId("CamaraApi", details.Id.ToString());

        var deputy = Deputy.Create(
            cpf,
            name,
            details.Nome,
            details.SiglaPartido,
            multiSourceId
        );

        await _deputyRepository.SaveDeputyAsync(deputy);
    }

    private async Task ProcessDeputiesExpensesForMonthsAsync(int year, IEnumerable<int> months)
    {
        var deputiesList = await _deputyRepository.GetDeputiesAsync();

        foreach (var deputy in deputiesList)
        {
            var deputyId = deputy.MultiSourceId.Ids.GetValueOrDefault("CamaraApi");
            if (string.IsNullOrWhiteSpace(deputyId)) continue;

            foreach (var month in months)
            {
                var expensesDtos = await _deputyProvider.GetDeputyExpensesAsync(deputyId, year, month);
                if (expensesDtos == null || expensesDtos.Count == 0)
                {
                    _logger.LogWarning("No expenses found for deputy {deputyId} in {year}-{month}",
                        deputyId, year, month);
                    continue;
                }

                var domainExpenses = BuildDomainExpenses(expensesDtos, deputy, deputyId);

                _logger.LogInformation("Saving {count} expenses for deputy {deputyId} in {year}-{month}",
                    domainExpenses.Count, deputyId, year, month);

                await _deputyRepository.SaveExpensesAsync(deputyId, domainExpenses);
            }
        }
    }

    private List<Expense> BuildDomainExpenses(IEnumerable<DeputyExpensesDto> expensesDtos, Deputy buyer, string deputyId)
    {
        var domainExpenses = new List<Expense>();

        foreach (var dto in expensesDtos)
        {
            var supplier = CreateSupplier(dto.CnpjCpfFornecedor, dto.NomeFornecedor, deputyId);
            var domainExpense = new Expense(
                dto.ValorDocumento,
                dto.DataDocumento,
                dto.TipoDespesa,
                buyer,
                supplier,
                dto.UrlDocumento
            );
            domainExpenses.Add(domainExpense);
        }

        return domainExpenses;
    }

    private Company CreateSupplier(string cnpjOrCpf, string supplierName, string deputyId)
    {
        if (!Cnpj.IsValidCnpj(cnpjOrCpf))
        {
            _logger.LogWarning("Invalid CNPJ {CNPJ} for deputy {deputyId} - supplier: {supplierName}",
                cnpjOrCpf, deputyId, supplierName);
            return Company.Create(new Cnpj(SharedConstants.NO_CNPJ_CONSTANT), supplierName);
        }
        else
        {
            return Company.Create(new Cnpj(cnpjOrCpf), supplierName);
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
