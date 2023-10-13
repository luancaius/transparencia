using ExternalAPI.Interfaces;
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
    
    public async Task<string> GetAllDeputiesRaw(int legislatura)
    {
        // call rest api get all deputies
        var deputies = await _DadosAbertosNewApi.GetAllDeputiesRaw(legislatura);
        _logger.Information(deputies);
        // call soap api get all deputies
        
        // save on mongo unique information
        throw new NotImplementedException();
    }

    public Task<string> GetDeputy(int legislatura, int id)
    {
        throw new NotImplementedException();
    }

    public Task<string> GetAllExpenses(int year, int month, int id)
    {
        throw new NotImplementedException();
    }

    public Task<string> GetAllWorkPresence(int year, int month, int id)
    {
        throw new NotImplementedException();
    }
}