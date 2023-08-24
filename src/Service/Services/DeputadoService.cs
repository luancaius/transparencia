using Repository;
using Repository.JsonEntity;

namespace Service.Services;

public class DeputadoService : RestService
{
    private Api1RestService _api1RestService;
    private Api2SoapService _api2SoapService;

    private JsonRepository _jsonRepository;

    public DeputadoService(JsonRepository jsonRepository, Api1RestService api1RestService,
        Api2SoapService api2SoapService)
    {
        _jsonRepository = jsonRepository;
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

                await _jsonRepository.InsertAsync(deputado_api1_mongo);
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
            var deputados_api1 = await _api2SoapService.GetAllAPI2();

            // var total = 0;
            // foreach (var deputado_item in deputados_api1.DeputadoList)
            // {
            //     var deputado_api1 = await _api1RestService.GetDeputadoAPI1(deputado_item.Id);
            //     var deputado_api1_mongo = new Api1DeputadoDtoMongo
            //         { Dados = deputado_api1, Nome = deputado_api1.NomeCivil };
            //     Console.WriteLine($"{total} - {deputado_api1_mongo.Nome}");
            //
            //     await _jsonRepository.InsertAsync(deputado_api1_mongo);
            //     total++;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}