using Entity.API1;
using Entity.Congresso;
using Service.Mappers;

namespace Service.Services;

public class Api1RestService : ApiService
{
    private String baseUrl = "https://dadosabertos.camara.leg.br/api/v2";

    private async Task<Api1DeputadoList> GetAllAPI1()
    {
        String apiUrl = "/deputados";
        String param = "?ordem=ASC&ordenarPor=nome";

        String deputados_raw = await GetAsync(baseUrl+apiUrl+param);

        var deputadosListApi1 = Api1Mapper.mapApi1ListToDto(deputados_raw);

        return deputadosListApi1;
    }
    
    private async Task<Api1DeputadoDto> GetDeputadoAPI1(int id)
    {
        String apiUrl = $"/deputados/{id}";

        String deputado_raw = await GetAsync(baseUrl+apiUrl);

        var api1DeputadoDadosDto = Api1Mapper.mapApi1ToDto(deputado_raw);

        return api1DeputadoDadosDto.DeputadoApi1;
    }
    
    private async Task<List<Deputado>> GetAll()
    {
        String apiUrl = "/deputados";
        String param = "?ordem=ASC&ordenarPor=nome";

        String deputados_raw = await GetAsync(baseUrl+apiUrl+param);

        var deputadosListApi1 = Api1Mapper.mapApi1ListToDto(deputados_raw);

        var deputados = Api1Mapper.mapListApi1ToEntity(deputadosListApi1);
        
        return deputados;
    }
    
    private async Task<Deputado> GetById(int Id)
    {
        String apiUrl = $"/deputados/{Id}";

        String deputado_raw = await GetAsync(baseUrl+apiUrl);

        var deputadoApi1 = Api1Mapper.mapApi1ToDto(deputado_raw);

        var deputado = Api1Mapper.mapApi1ToEntity(deputadoApi1);

        return deputado;
    }
}