using System.Text.Json;
using Deputies.Adapter.Out.ExternalAPI.Dtos;
using Deputies.Application.Dtos;
using Deputies.Application.Ports.Out;
using Microsoft.Extensions.Caching.Distributed;

namespace Deputies.Adapter.Out.ExternalAPI;

public class CamaraNewApiDeputyProvider : IDeputyProvider
{
    private readonly HttpClient _httpClient;
    private readonly IRedisCacheService _redisCache;
    private const string BaseUrl = "https://dadosabertos.camara.leg.br/api/v2";
    private readonly TimeSpan _cacheDuration = TimeSpan.FromHours(1);

    public CamaraNewApiDeputyProvider(HttpClient httpClient, IRedisCacheService redisCache)
    {
        _httpClient = httpClient;
        _redisCache = redisCache;
    }

    public async Task<IEnumerable<DeputyListItemDto>> GetDeputiesListAsync(int legislatura)
    {
        var requestUrl = $"{BaseUrl}/deputados?idLegislatura={legislatura}";

        // Try to get cached JSON
        var cachedJson = await _redisCache.GetAsync<string>(requestUrl);
        string content;

        if (!string.IsNullOrEmpty(cachedJson))
        {
            content = cachedJson;
        }
        else
        {
            var response = await _httpClient.GetAsync(requestUrl);
            response.EnsureSuccessStatusCode();

            content = await response.Content.ReadAsStringAsync();
            await _redisCache.SetAsync(requestUrl, content, _cacheDuration);
        }

        var apiResponse = JsonSerializer.Deserialize<ApiResponse<List<DeputadoListDto>>>(content);

        return apiResponse!.dados.Select(d => new DeputyListItemDto(
            d.id,
            d.nome,
            d.siglaPartido,
            d.siglaUf,
            d.idLegislatura,
            d.email
        ));
    }

    public async Task<DeputyDetailDto> GetDeputyDetailAsync(int deputyId)
    {
        var requestUrl = $"{BaseUrl}/deputados/{deputyId}";

        var cachedJson = await _redisCache.GetAsync<string>(requestUrl);
        string content;

        if (!string.IsNullOrEmpty(cachedJson))
        {
            content = cachedJson;
        }
        else
        {
            var response = await _httpClient.GetAsync(requestUrl);
            response.EnsureSuccessStatusCode();

            content = await response.Content.ReadAsStringAsync();
            await _redisCache.SetAsync(requestUrl, content, _cacheDuration);
        }

        var apiResponse = JsonSerializer.Deserialize<ApiResponse<DeputadoDetailDto>>(content);
        var dados = apiResponse!.dados;
        var status = dados.ultimoStatus;

        return new DeputyDetailDto(
            dados.id,
            dados.nomeCivil,
            dados.cpf,
            dados.sexo,
            dados.dataNascimento,
            dados.ufNascimento,
            dados.municipioNascimento,
            dados.escolaridade,
            status.nome,
            status.siglaPartido,
            status.siglaUf,
            status.idLegislatura,
            status.email
        );
    }

    public async Task<List<DeputyExpensesDto>> GetDeputyExpensesAsync(string deputyId, int year, int month)
    {
        var requestUrl = $"{BaseUrl}/deputados/{deputyId}/despesas?ano={year}&mes={month}&itens=10000";

        var cachedJson = await _redisCache.GetAsync<string>(requestUrl);
        string content;

        if (!string.IsNullOrEmpty(cachedJson))
        {
            content = cachedJson;
        }
        else
        {
            var response = await _httpClient.GetAsync(requestUrl);
            response.EnsureSuccessStatusCode();

            content = await response.Content.ReadAsStringAsync();
            await _redisCache.SetAsync(requestUrl, content, _cacheDuration);
        }

        var apiResponse = JsonSerializer.Deserialize<ApiResponse<List<ExpenseDto>>>(content)
            ?? throw new Exception("Failed to deserialize API response.");

        return apiResponse.dados.Select(e => new DeputyExpensesDto(
            e.Ano,
            e.Mes,
            e.TipoDespesa,
            e.CodDocumento,
            e.TipoDocumento,
            e.CodTipoDocumento,
            e.DataDocumento,
            e.NumDocumento,
            e.ValorDocumento,
            e.UrlDocumento,
            e.NomeFornecedor,
            e.CnpjCpfFornecedor,
            e.ValorLiquido
        )).ToList();
    }

    private class ApiResponse<T>
    {
        public T dados { get; set; }
    }
}

public interface IRedisCacheService
{
    Task<T?> GetAsync<T>(string key);
    Task SetAsync<T>(string key, T value, TimeSpan expiration);
}

public class RedisCacheService : IRedisCacheService
{
    private readonly IDistributedCache _cache;

    public RedisCacheService(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        var cachedData = await _cache.GetAsync(key);
        if (cachedData == null) return default;

        var jsonData = System.Text.Encoding.UTF8.GetString(cachedData);
        return JsonSerializer.Deserialize<T>(jsonData);
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan expiration)
    {
        var jsonData = JsonSerializer.Serialize(value);
        var data = System.Text.Encoding.UTF8.GetBytes(jsonData);

        await _cache.SetAsync(key, data, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expiration
        });
    }
}
