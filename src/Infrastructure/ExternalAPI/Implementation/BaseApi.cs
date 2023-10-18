using System.Net.Http.Headers;
using CacheDatabase.Interfaces;
using ExternalAPI.Interfaces;
using ExternalAPI.Utilities;

namespace ExternalAPI.Implementation;

public class BaseApi : IBaseApi
{
    private readonly HttpClient _httpClient;
    protected readonly ICacheRepository _cacheRepository;

    public BaseApi(ICacheRepository cacheService)
    {
        _cacheRepository = cacheService;
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<string> GetAsync(string apiUrl)
    {
        var cacheKey = $"RestService:GET:{apiUrl}";

        var cachedData = await _cacheRepository.GetStringAsync(cacheKey);
        if (!string.IsNullOrEmpty(cachedData))
        {
            Console.WriteLine($"Returning cache for key {cacheKey}");
            return cachedData;
        }

        Console.WriteLine($"Rest call to {apiUrl}");
        HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
        string data = await response.Content.ReadAsStringAsync();

        await _cacheRepository.SetStringAsync(cacheKey, data, TimeSpan.FromDays(30));

        return data;
    }

    public async Task<string> PostAsync(string apiUrl, HttpContent content)
    {
        var contentHash = HashUtil.GetHash(await content.ReadAsStringAsync());

        var cacheKey = $"RestService:POST:{apiUrl}:{contentHash}";

        var cachedData = await _cacheRepository.GetStringAsync(cacheKey);
        if (!string.IsNullOrEmpty(cachedData))
        {
            Console.WriteLine($"Returning cache for key {cacheKey}");
            return cachedData;
        }

        HttpResponseMessage response = await _httpClient.PostAsync(apiUrl, content);
        string responseData = await response.Content.ReadAsStringAsync();

        await _cacheRepository.SetStringAsync(cacheKey, responseData, TimeSpan.FromDays(30));

        return responseData;
    }
}