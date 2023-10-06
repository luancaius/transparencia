using ExternalAPI.Interfaces;
using Repositories.Interfaces;

namespace Repositories.Implementation;

public class SearchDeputyRepository : ISearchDeputyRepository
{
    private IDadosAbertosNewApi _DadosAbertosNewApi { get; set; }
    private IDadosAbertosOldApi _DadosAbertosOldApi { get; set; }

    public SearchDeputyRepository(IDadosAbertosNewApi dadosAbertosNewApi, IDadosAbertosOldApi dadosAbertosOldApi)
    {
        _DadosAbertosNewApi = dadosAbertosNewApi;
        _DadosAbertosOldApi = dadosAbertosOldApi;
    }
    
    public async Task<string> GetAllDeputiesRaw(int legislatura)
    {
        // call rest api get all deputies
        var deputies = await _DadosAbertosNewApi.GetAllDeputiesRaw(legislatura);
        
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