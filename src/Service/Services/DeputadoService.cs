using Entity.API1_Rest;
using Repository;
using Repository.JsonEntity;

namespace Service.Services;

public class DeputadoService : RestService
{
    private Api1RestService _api1RestService;
    private Api2SoapService _api2SoapService;

    private Api1MongoRepository _api1MongoRepository;
    private Api2MongoRepository _api2MongoRepository;

    public DeputadoService(Api1MongoRepository api1MongoRepository, Api2MongoRepository api2MongoRepository, Api1RestService api1RestService,
        Api2SoapService api2SoapService)
    {
        _api1MongoRepository = api1MongoRepository;
        _api2MongoRepository = api2MongoRepository;
        
        _api1RestService = api1RestService;
        _api2SoapService = api2SoapService;
    }

    public async Task Api1_GetAllDeputados_SaveOnMongoDB()
    {
        var deputados_api1 = await _api1RestService.GetAllAPI1();
        try
        {
            var total = 0;
            foreach (var deputado_item in deputados_api1.DeputadoList)
            {
                var deputado_api1 = await _api1RestService.GetDeputadoAPI1(deputado_item.Id);
                var deputado_api1_mongo = new Api1DeputadoDtoMongo
                    { Dados = deputado_api1, Nome = deputado_api1.NomeCivil };
                Console.WriteLine($"{total} - {deputado_api1_mongo.Nome}");

                await _api1MongoRepository.InsertAsync(deputado_api1_mongo);
                total++;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public async Task Api2_GetAllDeputados_SaveOnMongoDB()
    {
        try
        {
            var deputados_api2 = await _api2SoapService.GetAllAPI2();
            var total = 0;
            foreach (var deputado_item in deputados_api2)
            {
                var deputado_api2 = await _api2SoapService.GetDeputadoById(deputado_item.IdeCadastro, 57);
                var deputado_api2_mongo = new Api2DeputadoDtoMongo
                    { Dados = deputado_api2, Nome = deputado_api2.nomeCivil };
                Console.WriteLine($"{total} - {deputado_api2_mongo.Nome}");
                
                await _api2MongoRepository.InsertAsync(deputado_api2_mongo);
                total++;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public async Task<List<Api1DeputadoDespesa>> Api1_GetDeputadoDespesasByYear_SaveOnMongoDB(int year)
    {
        var deputado_despesas_list = new List<Api1DeputadoDespesa>();
        try
        {
            var deputados_api1 = await _api1MongoRepository.GetAll();

            var total = 0;
            foreach (var deputado_item in deputados_api1)
            {
                Console.WriteLine($"Getting despesas deputado {deputado_item.Nome}");
                var deputado_despesas = await _api1RestService.GetDeputadoDespesa(deputado_item.Dados.Id, year, 1);
                
                //Console.WriteLine($"{total} - {deputado_api1_mongo.Nome}");

                //await _api1MongoRepository.InsertAsync(deputado_api1_mongo);
                total++;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        return deputado_despesas_list;
    }
}