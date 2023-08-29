using Entity.API1_Rest;
using Entity.Congresso;
using Service.Mappers;

namespace Service.Services;

public class Api1RestService : RestService
{
    private String baseUrl = "https://dadosabertos.camara.leg.br/api/v2";

    public async Task<Api1DeputadoList> GetAllAPI1()
    {
        String apiUrl = "/deputados";
        String param = "?ordem=ASC&ordenarPor=nome";

        String deputados_raw = await GetAsync(baseUrl+apiUrl+param);

        var deputadosListApi1 = Api1Mapper.mapApi1ListToDto(deputados_raw);

        return deputadosListApi1;
    }
    
    public async Task<Api1DeputadoDto> GetDeputadoAPI1(int id)
    {
        String apiUrl = $"/deputados/{id}";

        String deputado_raw = await GetAsync(baseUrl+apiUrl);

        var api1DeputadoDadosDto = Api1Mapper.mapApi1ToDto(deputado_raw);

        return api1DeputadoDadosDto.DeputadoApi1;
    }
    
    public 
}