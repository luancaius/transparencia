using ExternalAPI.Interfaces;
using Serilog;

namespace ExternalAPI.Implementation;

public class DadosAbertosNewApi : IDadosAbertosNewApi
{
    private IBaseApi _baseApi { get; set; }
    private readonly ILogger _logger;
    public DadosAbertosNewApi(IBaseApi baseApi, ILogger logger)
    {
        _logger = logger.ForContext<DadosAbertosNewApi>();
        _baseApi = baseApi;
    }
    
    public async Task<string> GetAllDeputiesRaw(int legislatura)
    {
        _logger.Information("GetAllDeputiesRaw");
        var apiUrl = $"https://dadosabertos.camara.leg.br/api/v2/deputados?idLegislatura={legislatura}";
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