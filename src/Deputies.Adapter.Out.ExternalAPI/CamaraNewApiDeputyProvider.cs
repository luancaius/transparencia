using System.Text.Json;
using Deputies.Application.Dtos;
using Deputies.Application.Ports.Out;

namespace Deputies.Adapter.Out.ExternalAPI;

public class CamaraNewApiDeputyProvider : IDeputyProvider
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "https://dadosabertos.camara.leg.br/api/v2";

    public CamaraNewApiDeputyProvider(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<DeputyListItemDto>> GetDeputiesListAsync(int legislatura)
    {
        var response = await _httpClient.GetAsync($"{BaseUrl}/deputados?idLegislatura={legislatura}");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var apiResponse = JsonSerializer.Deserialize<ApiResponse<List<DeputadoListDto>>>(content);

        var listDeputies = apiResponse!.dados.Select(d => new DeputyListItemDto(
            d.id,
            d.nome,
            d.siglaPartido,
            d.siglaUf,
            d.idLegislatura,
            d.email
        ));
        return listDeputies;
    }

    public async Task<DeputyDetailDto> GetDeputyDetailAsync(int deputyId)
    {
        var response = await _httpClient.GetAsync($"{BaseUrl}/deputados/{deputyId}");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
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

    private class ApiResponse<T>
    {
        public T dados { get; set; }
    }

    private class DeputadoListDto
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string siglaPartido { get; set; }
        public string siglaUf { get; set; }
        public int idLegislatura { get; set; }
        public string email { get; set; }
    }

    private class DeputadoDetailDto
    {
        public int id { get; set; }
        public string nomeCivil { get; set; }
        public string cpf { get; set; }
        public string sexo { get; set; }
        public DateTime dataNascimento { get; set; }
        public string ufNascimento { get; set; }
        public string municipioNascimento { get; set; }
        public string escolaridade { get; set; }
        public UltimoStatusDto ultimoStatus { get; set; }
    }

    private class UltimoStatusDto
    {
        public string nome { get; set; }
        public string siglaPartido { get; set; }
        public string siglaUf { get; set; }
        public int idLegislatura { get; set; }
        public string email { get; set; }
    }
}