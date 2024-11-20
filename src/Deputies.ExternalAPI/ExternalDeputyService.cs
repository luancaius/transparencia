using System.Text.Json;
using Deputies.Application.Ports.Out;
using Deputies.Domain.Entities;

namespace Deputies.ExternalAPI
{
    public class ExternalDeputyService : IExternalDeputyService
    {
        private readonly HttpClient _httpClient;

        public ExternalDeputyService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Deputy>> GetAllDeputiesAsync(int year)
        {
            var response = await _httpClient.GetAsync($"https://api.example.com/deputies?year={year}");
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Deputy>>(jsonResponse) ?? throw new InvalidOperationException();
        }
    }
}