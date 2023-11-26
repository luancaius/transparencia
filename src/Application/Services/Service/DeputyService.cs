using Entities.ValueObject;
using NonRelationalDatabase.Interfaces;
using Repositories.DTO.NewApi.Expense;
using Repositories.DTO.NewApi.GetAll;
using Repositories.DTO.OldApi.GetAll;
using Repositories.DTO.OldApi.GetById;
using Repositories.Interfaces;
using Serilog;
using Services.DTO;
using Services.Interfaces;

namespace Services.Service;

public class DeputyService : IDeputyService
{
    private readonly INonRelationalDatabase _nonRelationalDatabase;
    private readonly ISearchDeputyRepository _searchDeputyRepository;
    private readonly ILogger _logger;

    public DeputyService(ISearchDeputyRepository searchDeputyRepository, ILogger logger,
        INonRelationalDatabase nonRelationalDatabase)
    {
        _searchDeputyRepository = searchDeputyRepository;
        _logger = logger.ForContext<DeputyService>();
        _nonRelationalDatabase = nonRelationalDatabase;
    }

    public async Task<DeputiesListDto> GetDeputiesListExternalApi(int legislatura)
    {
        _logger.Information($"GetDeputiesListExternalApi {legislatura}");
        var deputiesListNewApi = await _searchDeputyRepository.GetAllDeputiesNewApi(legislatura);

        var deputiesListOldApi = await _searchDeputyRepository.GetAllDeputiesOldApi(legislatura);

        var deputiesList = new DeputiesListDto(deputiesListOldApi, deputiesListNewApi);
        return deputiesList;
    }

    public async Task<DeputiesDetailListDto> GetDeputiesDetailListExternalApi(int legislatura)
    {
        var deputiesDetailListNewApi = new List<DeputyDetailNewApi>();
        DeputiesListNewApi deputiesListNewApi = await _searchDeputyRepository.GetAllDeputiesNewApi(legislatura);
        foreach (var deputy in deputiesListNewApi.DeputiesNewApi)
        {
            var id = Convert.ToInt32(deputy.Id);
            var deputyDetailNewApi = await _searchDeputyRepository.GetDeputyDetailNewApi(legislatura, id);
            deputiesDetailListNewApi.Add(deputyDetailNewApi);
        }

        var deputiesDetailListOldApi = new List<DeputyDetailOldApi>();
        DeputiesListOldApi deputiesListOldApi = await _searchDeputyRepository.GetAllDeputiesOldApi(legislatura);
        foreach (var deputy in deputiesListOldApi.DeputiesOldApi)
        {
            var id = Convert.ToInt32(deputy.IdeCadastro);
            var deputyDetailOldApi = await _searchDeputyRepository.GetDeputyDetailOldApi(legislatura, id);
            deputiesDetailListOldApi.Add(deputyDetailOldApi);
        }

        var deputiesDetailList = new DeputiesDetailListDto(deputiesDetailListOldApi, deputiesDetailListNewApi);

        return deputiesDetailList;
    }

    public async Task RefreshNewApi(int year)
    {
        var legislaturaObj = Legislatura.CriarLegislaturaPorAno(year);
        var legislatura = legislaturaObj.Numero;
        _logger.Information($"RefreshNewApi {legislatura} {year}");
        int counter = 0;

        DeputiesListNewApi deputiesListNewApi = await _searchDeputyRepository.GetAllDeputiesNewApi(legislatura);
        var total = deputiesListNewApi.DeputiesNewApi.Count;
        foreach (var deputy in deputiesListNewApi.DeputiesNewApi)
        {
            try
            {
                var id = Convert.ToInt32(deputy.Id);
                _logger.Debug($"starting deputy details new api {id} {counter}");
                DeputyDetailNewApi deputyDetailNewApi =
                    await _searchDeputyRepository.GetDeputyDetailNewApi(legislatura, id);
                await _nonRelationalDatabase.CheckAndUpdate(deputyDetailNewApi);
                var currentMonth = DateTime.Now.Year == year ? DateTime.Now.Month : 12;
                for (int month = 1; month <= currentMonth; month++)
                {
                    _logger.Debug($"getting expenses deputy {id} {counter}/{total}");
                    DeputyExpense deputyExpense = await _searchDeputyRepository.GetDeputyExpense(year, month, id);
                    await _nonRelationalDatabase.CheckAndUpdate(deputyExpense);
                }
                counter++;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error on deputy new api {deputy.Id}");
            }
        }
        _logger.Information("Database RefreshNewApi refreshed");
    }

    public async Task RefreshOldApi(int year)
    {
        var legislaturaObj = Legislatura.CriarLegislaturaPorAno(year);
        var legislatura = legislaturaObj.Numero;
        _logger.Information($"RefreshNewApi {legislatura} {year}");
        int counter = 0;
        
        var deputiesListOldApi = await _searchDeputyRepository.GetAllDeputiesOldApi(legislatura);
        var total = deputiesListOldApi.DeputiesOldApi.Count;
        foreach (var deputy in deputiesListOldApi.DeputiesOldApi)
        {
            try
            {
                var id = deputy.IdeCadastro;
                var matricula = deputy.Matricula;
                _logger.Debug($"starting deputy details old api {id} {counter}/{total}");
                DeputyDetailOldApi deputyDetailOldApi =
                    await _searchDeputyRepository.GetDeputyDetailOldApi(legislatura, id);
                await _nonRelationalDatabase.CheckAndUpdate(deputyDetailOldApi);
                var currentMonth = DateTime.Now.Year == year ? DateTime.Now.Month : 12;
                for (int month = 1; month <= currentMonth; month++)
                {
                    _logger.Debug($"getting work presence deputy {matricula} {counter}/{total}");
                    var deputyWorkPresence =
                        await _searchDeputyRepository.GetDeputyWorkPresence(year, month, matricula);
                    await _nonRelationalDatabase.CheckAndUpdate(deputyWorkPresence);
                }
                counter++;
            }
            catch (Exception e)
            {
                _logger.Error(e, $"Error on deputy old api {deputy.IdeCadastro}");
            }
        }
        _logger.Information("Database RefreshOldApi refreshed");
    }
    
    public async Task RefreshAllMongoDb(int year)
    {
        await RefreshNewApi(year);
        await RefreshOldApi(year);
    }

    public Task RefreshRelationalDbFromNonRelationalDb(int year)
    {
        // create models for deputy details, expenses and work presence
        // get all deputies from non relational db
        // for each deputy, get details, expenses and work presence
        // save each one on relational db
        throw new NotImplementedException();
    }
}