using System.Net.Http.Headers;
using Service.Interfaces;

namespace Service.Services
{
    public class RestService
    {
        private readonly HttpClient _httpClient;
        protected readonly IRedisCacheService _cacheService;

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
                Console.WriteLine($"Returning cache for key {apiUrl}");
                return cachedData;
            }
            Console.WriteLine($"Rest call to {apiUrl}");
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
            string data = await response.Content.ReadAsStringAsync();

            await _cacheService.SetStringAsync(cacheKey, data, TimeSpan.FromDays(10));

            return data;
        }

    }
}
