using ExternalAPI.Interfaces;
using Repositories.DTO;
using Repositories.DTO.NewApi;
using Repositories.DTO.NewApi.GetAll;
using Repositories.DTO.OldApi.GetAll;
using Repositories.DTO.OldApi.GetById;
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

    public async Task<DeputyDetailOldApi> GetDeputyDetailOldApi(int id, int legislatura)
    {
        _logger.Information("GetDeputyDetailOldApi");
        var deputyDetailOldApiString = await _DadosAbertosOldApi.GetDeputyRaw(id, legislatura);    
        var deputyDetailOldApi = new DeputyDetailOldApi(deputyDetailOldApiString);
        return deputyDetailOldApi;
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