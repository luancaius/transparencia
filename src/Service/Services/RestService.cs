using System.Net.Http.Headers;
using System.Text;
using Service.Mappers;

namespace Service.Services
{
    public class RestService
    {
        private readonly HttpClient _httpClient;

        public RestService()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
        }
        
        public async Task<String> GetAsync(string apiUrl)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            String data = await response.Content.ReadAsStringAsync();
            
            return data;
        }
    }
}
