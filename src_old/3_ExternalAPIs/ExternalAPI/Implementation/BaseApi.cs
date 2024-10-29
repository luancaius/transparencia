using System.Net;
using System.Net.Http.Headers;
using CacheDatabase.Interfaces;
using ExternalAPI.Interfaces;
using ExternalAPI.Utilities;
using Serilog;

namespace ExternalAPI.Implementation;

public class BaseApi : IBaseApi
{
    private readonly HttpClient _httpClient;
    protected readonly ICacheRepository _cacheRepository;
    private readonly ILogger _logger;
    private readonly TimeSpan _timespan = TimeSpan.FromDays(365);

    public BaseApi(ICacheRepository cacheService, ILogger logger)
    {
        _cacheRepository = cacheService;
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        _logger = logger.ForContext<BaseApi>();
    }

    public async Task<string> GetAsync(string apiUrl)
    {
        _logger.Information("GetAsync");
        var cacheKey = $"RestService:GET:{apiUrl}";

        var cachedData = await _cacheRepository.GetStringAsync(cacheKey);
        if (!string.IsNullOrEmpty(cachedData))
        {
            _logger.Information($"Returning cache for key {cacheKey}");
            return cachedData;
        }

        _logger.Information($"Rest call for {apiUrl}");
        HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
        string data = String.Empty;
        if (response.StatusCode == HttpStatusCode.OK)
        {
            data = await response.Content.ReadAsStringAsync();

            _logger.Information($"GetAsync setting key {cacheKey}");
            await _cacheRepository.SetStringAsync(cacheKey, data, _timespan);
        }
        else
        {
            _logger.Error($"Error calling {apiUrl}: {response.StatusCode}");
        }

        return data;
    }

    public async Task<string> PostAsync(string apiUrl, HttpContent content)
    {
        _logger.Information("PostAsync");

        var contentHash = HashUtil.GetHash(await content.ReadAsStringAsync());

        var cacheKey = $"RestService:POST:{apiUrl}:{contentHash}";

        var cachedData = await _cacheRepository.GetStringAsync(cacheKey);
        if (!string.IsNullOrEmpty(cachedData))
        {
            _logger.Information($"Returning cache for key {cacheKey}");
            return cachedData;
        }

        HttpResponseMessage response = await _httpClient.PostAsync(apiUrl, content);
        string data = String.Empty;
        if (response.StatusCode == HttpStatusCode.OK)
        {
            data = await response.Content.ReadAsStringAsync();
            _logger.Information($"PostAsync setting key {cacheKey}");
            await _cacheRepository.SetStringAsync(cacheKey, data, _timespan);
        }
        else
        {
            _logger.Error($"Error calling {apiUrl}: {response.StatusCode}");
        }
        
        return data;
    }
}