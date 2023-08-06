using System.Net.Http.Headers;
using System.Text;
using Service.Mappers;

namespace Service.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // Method to make a GET request and parse the JSON response
        public async Task<T> GetAsync<T>(string apiUrl)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            ApiResponseMapper responseMapper = new ApiResponseMapper();
            T mappedData = await responseMapper.MapResponse<T>(response);

            return mappedData;
        }
        
        public async Task<String> GetAsync(string apiUrl)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            String data = await response.Content.ReadAsStringAsync();
            
            return data;
        }
        public async Task<T> PostAsync<T>(string apiUrl, object data)
        {
            string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                T responseData = await response.Content.ReadAsAsync<T>();
                return responseData;
            }

            throw new Exception($"Failed to call the API. Status code: {response.StatusCode}");
        }
    }
}
