using ExternalAPI.Interfaces;
using Repositories.DTO.NewApi.Expense;
using Repositories.DTO.NewApi.GetAll;
using Repositories.DTO.NewApi.GetById;
using Repositories.DTO.OldApi.GetAll;
using Repositories.DTO.OldApi.GetById;
using Repositories.DTO.OldApi.WorkPresence;
using Repositories.Interfaces;
using Serilog;

namespace Repositories.Implementation;

public class SearchDeputyRepository : ISearchDeputyRepository
{
    private IDadosAbertosNewApi _DadosAbertosNewApi { get; }
    private IDadosAbertosOldApi _DadosAbertosOldApi { get; }
    private readonly ILogger _logger;

    public SearchDeputyRepository(IDadosAbertosNewApi dadosAbertosNewApi, IDadosAbertosOldApi dadosAbertosOldApi, ILogger logger)
    {
        _DadosAbertosNewApi = dadosAbertosNewApi;
        _DadosAbertosOldApi = dadosAbertosOldApi;
        _logger = logger.ForContext<SearchDeputyRepository>();
    }

    public async Task<DeputiesListOldApi> GetAllDeputiesOldApi(int legislatura)
    {
        _logger.Information("GetAllDeputiesOldApi");
        var deputiesOldApiString = await _DadosAbertosOldApi.GetAllDeputiesRaw(legislatura);    
        var deputiesListOldApi = new DeputiesListOldApi(deputiesOldApiString);
        return deputiesListOldApi;
    }

    public async Task<DeputiesListNewApi> GetAllDeputiesNewApi(int legislatura)
    {
        _logger.Information("GetAllDeputiesNewApi");
        var deputiesNewApiString = await _DadosAbertosNewApi.GetAllDeputiesRaw(legislatura);    
        var deputiesListNewApi = new DeputiesListNewApi(deputiesNewApiString);
        return deputiesListNewApi;
    }

    public async Task<DeputyDetailOldApi> GetDeputyDetailOldApi(int legislatura, int id)
    {
        _logger.Information("GetDeputiesDetailOldApi");
        var deputyDetailString = await _DadosAbertosOldApi.GetDeputyRaw(legislatura, id);
        var deputyDetail = new DeputyDetailOldApi(deputyDetailString, id);
        return deputyDetail;
    }

    public async Task<DeputyDetailNewApi> GetDeputyDetailNewApi(int legislatura, int id)
    {
        _logger.Information("GetDeputyDetailNewApi");
        var deputyDetailString = await _DadosAbertosNewApi.GetDeputyRaw(id);
        var deputyDetail = new DeputyDetailNewApi(deputyDetailString);
        return deputyDetail;    
    }

    public async Task<DeputyExpense> GetDeputyExpense(int year, int month, int id)
    {
        _logger.Information($"GetDeputyExpense {year} {month} {id}");
        var deputyExpenseRaw = await _DadosAbertosNewApi.GetDeputyExpensesRaw(year, month, id);
        var deputyExpense = new DeputyExpense(deputyExpenseRaw, year, month, id);
        return deputyExpense;     
    }

    public async Task<DeputyWorkPresence> GetDeputyWorkPresence(int year, int month, int id)
    {
        _logger.Information($"GetDeputyWorkPresence {year} {month} {id}");
        var deputyDeputyWorkPresenceRaw = await _DadosAbertosOldApi.GetDeputyWorkPresenceRaw(year, month, id);
        var deputyDeputyWorkPresence = new DeputyWorkPresence(deputyDeputyWorkPresenceRaw, year, month, id);
        return deputyDeputyWorkPresence;
    }
}