using System.Text.Json;
using Deputies.Application.Ports.Out;
using Deputies.Domain.Entities;
using Deputies.Domain.ValueObjects;

namespace Deputies.ExternalAPI
{
    public class CamaraNewApiDeputyProvider : IDeputyProvider
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://dadosabertos.camara.leg.br/api/v2";

        public CamaraNewApiDeputyProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Deputy>> GetDeputiesAsync(int year)
        {
            var legislatura = CalculateLegislatura(year);
            var response = await _httpClient.GetAsync($"{BaseUrl}/deputados?idLegislatura={legislatura}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonSerializer.Deserialize<ApiResponse>(content);

            return apiResponse.dados.Select(d => new Deputy(
                new Person(new Cpf("00000000000"), new Name(d.nome)), // We'll need to fetch details for complete info
                d.nome,
                d.siglaPartido,
                new MultiSourceId("CamaraApi", d.id.ToString())
            ));
        }

        private int CalculateLegislatura(int year)
        {
            // Base calculation: 2023-2026 is the 57th legislature
            int baseYear = 2023;
            int baseLegislatura = 57;
            int yearDiff = year - baseYear;
            return baseLegislatura + (yearDiff / 4);
        }

        private class ApiResponse
        {
            public List<DeputadoDto> dados { get; set; }
        }

        private class DeputadoDto
        {
            public int id { get; set; }
            public string nome { get; set; }
            public string siglaPartido { get; set; }
        }
    }
}