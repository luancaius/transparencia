using System.Runtime.InteropServices.JavaScript;
using Entity.API1_Rest;
using Entity.API2_Soap.GetListaPresenca;
using Repository.JsonEntity;
using Repository.Repositories.Mongo;
using Service.Interfaces;

namespace Service.Services;

public class DeputadoService
{
    private Api1Service _api1Service;
    private Api2Service _api2SoapService;

    private Api1DeputadoMongoRepository _api1MongoRepository;
    private Api2DeputadoMongoRepository _api2MongoRepository;
    private Api1DeputadoDespesasMongoRepository _api1DeputadoDespesasMongoRepository;
    private Api2DeputadoListaPresencaMongoRepository _api2DeputadoListaPresencaMongoRepository;

    public DeputadoService(Api1DeputadoMongoRepository api1MongoRepository, 
        Api2DeputadoMongoRepository api2MongoRepository, Api1DeputadoDespesasMongoRepository api1DeputadoDespesasMongoRepository,
        Api2DeputadoListaPresencaMongoRepository api2DeputadoListaPresencaMongoRepository, Api1Service api1Service, Api2Service api2SoapService)
    {
        _api1MongoRepository = api1MongoRepository;
        _api2MongoRepository = api2MongoRepository;

        _api1Service = api1Service;
        _api2SoapService = api2SoapService;

        _api1DeputadoDespesasMongoRepository = api1DeputadoDespesasMongoRepository;
        _api2DeputadoListaPresencaMongoRepository = api2DeputadoListaPresencaMongoRepository;
    }

    public async Task Api1_GetAllDeputados_SaveOnMongoDB()
    {
        var deputadosApi1 = await _api1Service.GetAllAPI1();
        try
        {
            var total = 0;
            foreach (var deputadoItem in deputadosApi1.DeputadoList)
            {
                var deputadoApi1 = await _api1Service.GetDeputadoAPI1(deputadoItem.Id);
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

    public async Task Api1_GetDeputadoDespesasByYear_SaveOnMongoDB(int year, bool isWholeYear = false)
    {
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
                var currentMonth = isWholeYear ? 12 : DateTime.Now.Month;
                for (int month = 1; month <= currentMonth; month++)
                {
                    var deputadoDespesasMes = await _api1Service.GetDeputadoDespesa(deputadoItem.Dados.Id, year, month);
                    if (deputadoDespesasMes.Count > 0)
                        await _api1DeputadoDespesasMongoRepository.InsertManyAsync(deputadoDespesasMes);
                    deputadoDespesasAno.AddRange(deputadoDespesasMes);
                }

                var sumDespesas = deputadoDespesasAno.Sum(a => a.ValorDocumento);
                Console.WriteLine($"total despesas - {deputadoDespesasAno.Count} - soma:{sumDespesas}");
                total++;
            }
        }
        catch (Exception e)
        {
            if (deputadoCurrent != null)
                Console.WriteLine($"deputado: {deputadoCurrent.Nome} {deputadoCurrent.Dados.Id}");
            Console.WriteLine(e.Message);
        }
    }

    public async Task Api2_GetListaPresencaDeputado_SaveOnMongoDB(int year, bool isWholeYear = false)
    {
        Api2DeputadoDtoMongo deputadoCurrent = null;
        try
        {
            var deputadosApi2 = await _api2MongoRepository.GetAll();

            var total = 1;
            foreach (var deputadoItem in deputadosApi2)
            {
                deputadoCurrent = deputadoItem;
                Console.WriteLine($"{total} - Getting lista presenca deputado: {deputadoItem.Nome} matricula: {deputadoItem.Dados.numLegislatura}");
                var currentMonth = isWholeYear ? 12 : DateTime.Now.Month;
                for (int month = 1; month <= currentMonth; month++)
                {
                    var beginMonth = new DateTime(year, month, 1);
                    var dayEndMonth = DateTime.DaysInMonth(year, month);
                    var endMonth = new DateTime(year, month, dayEndMonth);
                    var matricula = deputadoItem.Dados.idParlamentarDeprecated;
                    DeputadoListaPresencaSoap deputadoListaPresenca = await _api2SoapService.GetDeputadoListaPresenca(beginMonth, endMonth, matricula);
                    var listaDeputadoPresenca = new List<DeputadoItemPresencaSoap>();
                    foreach (var sessaoDia in deputadoListaPresenca.DiasDeSessoes)
                    {
                        var deputadoItemPresenca = new DeputadoItemPresencaSoap{NumMatriculaDeputado = deputadoListaPresenca.CarteiraParlamentar};
                        deputadoItemPresenca.Data = sessaoDia.Data;
                        deputadoItemPresenca.Justificativa = sessaoDia.Justificativa;
                        deputadoItemPresenca.QtdeSessoes = sessaoDia.QtdeSessoes;
                        deputadoItemPresenca.FrequenciaNoDia = sessaoDia.FrequenciaNoDia;
                        deputadoItemPresenca.Sessoes = sessaoDia.Sessoes;
                        listaDeputadoPresenca.Add(deputadoItemPresenca);
                    }
                    await _api2DeputadoListaPresencaMongoRepository.InsertManyAsync(listaDeputadoPresenca);
                }
            }
        }
        catch (Exception e)
        {
            if (deputadoCurrent != null)
                Console.WriteLine($"deputado: {deputadoCurrent.Nome} {deputadoCurrent.Dados.idParlamentarDeprecated}");
            Console.WriteLine(e.Message);
        }
    }
}