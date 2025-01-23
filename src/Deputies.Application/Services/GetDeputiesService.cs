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
                var details = await _deputyProvider.GetDeputyDetailAsync(deputyListItem.Id);

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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing deputy {Id}", deputyListItem.Id);
            }
        }
    }

    public async Task ProcessDeputiesExpensesByYearAsync(int year)
    {
        var currentExpense = string.Empty;
        DeputyExpensesDto currentDtoExpense;
        try
        {
            var deputiesList = await _deputyRepository.GetDeputiesAsync();

            foreach (var deputy in deputiesList)
            {
                var deputyId = deputy.MultiSourceId.Ids.GetValueOrDefault("CamaraApi");
                if (string.IsNullOrWhiteSpace(deputyId))
                    continue;
                
                var months = Enumerable.Range(1, 12);
                if (DateTime.Now.Year == year)
                {
                    months = Enumerable.Range(1, DateTime.Now.Month);
                }
                foreach (var month in months)
                {
                    List<DeputyExpensesDto> expensesDtos = await _deputyProvider.GetDeputyExpensesAsync(
                        deputyId,
                        year,
                        month
                    );

                    if (expensesDtos == null || expensesDtos.Count == 0)
                    {
                        _logger.LogWarning($"No expenses found for deputy {deputyId}");
                        continue;
                    }

                    var buyer = deputy;
                    var domainExpenses = new List<Expense>();

                    foreach (var dto in expensesDtos)
                    {
                        currentDtoExpense = dto;
                        currentExpense =
                            $"cnpj:{dto.CnpjCpfFornecedor} - valor:{dto.ValorDocumento} - deputyId:{deputyId} {dto.UrlDocumento}";
                        Company supplier;
                        if (Cnpj.IsValidCnpj(dto.CnpjCpfFornecedor) == false)
                        {
                            _logger.LogWarning(
                                $"Invalid CNPJ {dto.CnpjCpfFornecedor} - {deputyId} - fornecedor: {dto.NomeFornecedor}");
                            supplier = Company.Create(new Cnpj(SharedConstants.NO_CNPJ_CONSTANT), dto.NomeFornecedor);
                        }
                        else
                        {
                            supplier = Company.Create(new Cnpj(dto.CnpjCpfFornecedor), dto.NomeFornecedor);
                        }

                        var domainExpense = new Expense(
                            amount: dto.ValorDocumento,
                            date: dto.DataDocumento,
                            description: dto.TipoDespesa,
                            buyer: buyer,
                            supplier: supplier,
                            urlDocument: dto.UrlDocumento
                        );

                        domainExpenses.Add(domainExpense);
                    }

                    _logger.LogInformation("Saving expenses for deputy {deputyId}", deputyId);
                    await _deputyRepository.SaveExpensesAsync(deputyId, domainExpenses);
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error processing deputies expenses for year {year}, {currentExpense}");
        }
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

            var buyer = deputy;
            var domainExpenses = new List<Expense>();

            foreach (var dto in expensesDtos)
            {
                var supplier = Company.Create(new Cnpj(dto.CnpjCpfFornecedor), dto.NomeFornecedor);
                var domainExpense = new Expense(
                    amount: dto.ValorDocumento,
                    date: dto.DataDocumento,
                    description: dto.TipoDespesa,
                    buyer: buyer,
                    supplier: supplier,
                    urlDocument: dto.UrlDocumento
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