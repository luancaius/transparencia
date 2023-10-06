using ExternalAPI.Interfaces;

namespace ExternalAPI.Implementation;

public class DadosAbertosNewApi : IDadosAbertosNewApi
{
    private IBaseApi _baseApi { get; set; }
    public DadosAbertosNewApi(IBaseApi baseApi)
    {
        _baseApi = baseApi;
    }
    
    public async Task<string> GetAllDeputiesRaw(int legislatura)
    {
        var apiUrl = $"https://dadosabertos.camara.leg.br/api/v2/deputados?legislatura={legislatura}";
        return await _baseApi.GetAsync(apiUrl);
    }
    
    public async Task<string> GetDeputyRaw(int id)
    {
        var apiUrl = $"https://dadosabertos.camara.leg.br/api/v2/deputados/{id}";
        return await _baseApi.GetAsync(apiUrl);
    }
    
    public async Task<string> GetDeputyExpensesRaw(int year, int id)
    {
        var apiUrl = $"https://dadosabertos.camara.leg.br/api/v2/deputados/{id}/despesas?ano={year}";
        return await _baseApi.GetAsync(apiUrl);
    }
}