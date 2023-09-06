using Entity.API1_Rest;
using Repository.JsonEntity;
using Repository.Repositories.Mongo;

namespace Service.Services;

public class DeputadoService : RestService
{
    private Api1RestService _api1RestService;
    private Api2SoapService _api2SoapService;

    private Api1DeputadoMongoRepository _api1MongoRepository;
    private Api2DeputadoMongoRepository _api2MongoRepository;
    private Api1Deputado_DespesasMongoRepository _api1DeputadoDespesasMongoRepository;

    public DeputadoService(Api1DeputadoMongoRepository api1MongoRepository, Api2DeputadoMongoRepository api2MongoRepository, Api1RestService api1RestService,
        Api2SoapService api2SoapService, Api1Deputado_DespesasMongoRepository api1DeputadoDespesasMongoRepository)
    {
        _api1MongoRepository = api1MongoRepository;
        _api2MongoRepository = api2MongoRepository;
        
        _api1RestService = api1RestService;
        _api2SoapService = api2SoapService;

        _api1DeputadoDespesasMongoRepository = api1DeputadoDespesasMongoRepository;
    }

    public async Task Api1_GetAllDeputados_SaveOnMongoDB()
    {
        var deputadosApi1 = await _api1RestService.GetAllAPI1();
        try
        {
            var total = 0;
            foreach (var deputadoItem in deputadosApi1.DeputadoList)
            {
                var deputadoApi1 = await _api1RestService.GetDeputadoAPI1(deputadoItem.Id);
                var deputadoApi1Mongo = new Api1DeputadoDtoMongo
                    { Dados = deputadoApi1, Nome = deputadoApi1.NomeCivil };
                Console.WriteLine($"{total} - {deputadoApi1Mongo.Nome}");

                await _api1MongoRepository.InsertAsync(deputadoApi1Mongo);
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
            var deputadosApi2 = await _api2SoapService.GetAllAPI2();
            var total = 0;
            foreach (var deputadoItem in deputadosApi2)
            {
                var deputadoApi2 = await _api2SoapService.GetDeputadoById(deputadoItem.IdeCadastro, 57);
                var deputadoApi2Mongo = new Api2DeputadoDtoMongo
                    { Dados = deputadoApi2, Nome = deputadoApi2.nomeCivil };
                Console.WriteLine($"{total} - {deputadoApi2Mongo.Nome}");
                
                await _api2MongoRepository.InsertAsync(deputadoApi2Mongo);
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
        var deputadoDespesasList = new List<Api1DeputadoDespesa>();
        Api1DeputadoDtoMongo deputadoCurrent = null;
        try
        {
            var deputadosApi1 = await _api1MongoRepository.GetAll();

            var total = 1;
            foreach (var deputadoItem in deputadosApi1)
            {
                deputadoCurrent = deputadoItem;
                var deputadoDespesasAno = new List<Api1DeputadoDespesa>();
                Console.WriteLine($"{total} - Getting despesas deputado {deputadoItem.Nome} {deputadoItem.Dados.Id}");
                for (int month = 1; month <= 12; month++)
                {
                    var deputadoDespesasMes = await _api1RestService.GetDeputadoDespesa(deputadoItem.Dados.Id, year, month);
                    deputadoDespesasAno.AddRange(deputadoDespesasMes);
                }

                var sumDespesas = deputadoDespesasAno.Sum(a => a.ValorDocumento);
                Console.WriteLine($"total despesas - {deputadoDespesasAno.Count} - soma:{sumDespesas}");
                if (deputadoDespesasAno.Count > 0)
                    await _api1DeputadoDespesasMongoRepository.InsertManyAsync(deputadoDespesasAno);
                total++;
            }
        }
        catch (Exception e)
        {
            if (deputadoCurrent != null)
                Console.WriteLine($"deputado: {deputadoCurrent.Nome} {deputadoCurrent.Dados.Id}");
            Console.WriteLine(e.Message);
        }
        return deputadoDespesasList;
    }
}