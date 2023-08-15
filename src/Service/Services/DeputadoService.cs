using Entity.API1;
using Entity.Congresso;
using Repository;
using Repository.JsonEntity;
using Service.Mappers;

namespace Service.Services;

public class DeputadoService : ApiService
{
    private String baseUrl = "https://dadosabertos.camara.leg.br/api/v2";

    private JsonRepository _jsonRepository;
    public DeputadoService(JsonRepository jsonRepository)
    {
        _jsonRepository = jsonRepository;
    }
    
    public async Task GetLatestDeputados()
    {
        var deputados = await GetAll();
        foreach (var deputado_item in deputados)
        {
            var deputado = await GetById(deputado_item.Id);
            
        }
    }
    
    public async Task SaveOnMongoDB()
    {
        var deputados_api1 = await GetAllAPI1();
        foreach (var deputado_item in deputados_api1.DeputadoList)
        {
            var deputado_api1 = await GetDeputadoAPI1(deputado_item.Id);
            var deputado_api1_mongo = new Api1DeputadoDtoMongo{Api1DeputadoDto = deputado_api1};
            await _jsonRepository.InsertAsync(deputado_api1_mongo);
        }
    }
    
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