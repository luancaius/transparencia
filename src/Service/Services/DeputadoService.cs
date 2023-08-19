using Entity.API1;
using Entity.Congresso;
using MongoDB.Bson;
using MongoDB.Driver;
using Repository;
using Repository.JsonEntity;
using Service.Mappers;

namespace Service.Services;

public class DeputadoService : ApiService
{
    private Api1RestService _api1RestService;
    private JsonRepository _jsonRepository;
    public DeputadoService(JsonRepository jsonRepository, Api1RestService api1RestService)
    {
        _jsonRepository = jsonRepository;
        _api1RestService = api1RestService;
    }
    
    public async Task GetLatestDeputados()
    {
        var deputados = await _api1RestService.GetAll();
        foreach (var deputado_item in deputados)
        {
            var deputado = await _api1RestService.GetById(deputado_item.Id);
            
        }
    }
    
    public async Task SaveOnMongoDB()
    {
        var deputados_api1 = await _api1RestService.GetAllAPI1();
        try
        {
            var total = 0;
            foreach (var deputado_item in deputados_api1.DeputadoList)
            {
                var deputado_api1 = await _api1RestService.GetDeputadoAPI1(deputado_item.Id);
                var deputado_api1_mongo = new Api1DeputadoDtoMongo { Dados = deputado_api1, Nome = deputado_api1.NomeCivil};
                Console.WriteLine($"{total} - {deputado_api1_mongo.Nome}");
                
                await _jsonRepository.InsertAsync(deputado_api1_mongo);
                total++;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
    
    
}