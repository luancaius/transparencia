using System.Net.Http.Headers;
using Service.Interfaces;

namespace Service.Services
{
    public class RestService
    {
        private readonly HttpClient _httpClient;
        private readonly IRedisCacheService _cacheService;

        public RestService(IRedisCacheService cacheService)
        {
            _cacheService = cacheService;
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
        }
        
        public async Task<string> GetAsync(string apiUrl)
        {
            var cacheKey = $"Api1RestService:{apiUrl}";

            var cachedData = await _cacheService.GetStringAsync(cacheKey);

            if (!string.IsNullOrEmpty(cachedData))
            {
                return cachedData;
            }

            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
            string data = await response.Content.ReadAsStringAsync();

            await _cacheService.SetStringAsync(cacheKey, data, TimeSpan.FromHours(1));

            return data;
        }

    }
}
