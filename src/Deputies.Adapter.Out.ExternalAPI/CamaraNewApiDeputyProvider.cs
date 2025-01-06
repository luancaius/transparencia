using System.Text.Json;
using Deputies.Adapter.Out.ExternalAPI.Dtos;
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

    public async Task<List<DeputyExpensesDto>> GetDeputyExpensesAsync(string deputyId, int year, int month)
    {
        var url = $"{BaseUrl}/deputados/{deputyId}/despesas?ano={year}&mes={month}&itens=10000";
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var apiResponse = JsonSerializer.Deserialize<ApiResponse<List<ExpenseDto>>>(content);

        if (apiResponse is null)
        {
            throw new Exception("Failed to deserialize API response.");
        }

        var deputyExpenses = apiResponse.dados.Select(e => new DeputyExpensesDto(
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
        
        return deputyExpenses;
    }

    private class ApiResponse<T>
    {
        public T dados { get; set; }
    }
}