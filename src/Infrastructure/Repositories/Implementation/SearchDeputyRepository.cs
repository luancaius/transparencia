using ExternalAPI.Interfaces;
using Repositories.DTO;
using Repositories.Interfaces;
using Serilog;

namespace Repositories.Implementation;

public class SearchDeputyRepository : ISearchDeputyRepository
{
    private IDadosAbertosNewApi _DadosAbertosNewApi { get; set; }
    private IDadosAbertosOldApi _DadosAbertosOldApi { get; set; }
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
        var deputiesNewApi = await _DadosAbertosOldApi.GetAllDeputiesRaw(legislatura);    
        var deputiesListOldApi = new DeputiesListOldApi(deputiesNewApi);
        return deputiesListOldApi;
    }

    public async Task<DeputiesListNewApi> GetAllDeputiesNewApi(int legislatura)
    {
        _logger.Information("GetAllDeputiesNewApi");
        var deputiesNewApi = await _DadosAbertosNewApi.GetAllDeputiesRaw(legislatura);    
        var deputiesListNewApi = new DeputiesListNewApi(deputiesNewApi);
        return deputiesListNewApi;
    }

    public Task<string> GetDeputy(int legislatura, int id)
    {
        _logger.Information("GetAllDeputiesNewApi $legislatura $id");
        throw new NotImplementedException();
    }

    public Task<string> GetAllExpenses(int year, int month, int id)
    {
        _logger.Information("GetAllExpenses $year $month $id");
        throw new NotImplementedException();
    }

    public Task<string> GetAllWorkPresence(int year, int month, int id)
    {
        _logger.Information("GetAllWorkPresence $year $month $id");
        throw new NotImplementedException();
    }
}