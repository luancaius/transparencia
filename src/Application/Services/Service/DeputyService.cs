using NonRelationalDatabase.Interfaces;
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
        _logger.Information("GetDeputiesListExternalApi $legislatura");
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

    public async Task<string> GetDeputyRaw(int legislatura, int id)
    {
        _logger.Information("GetDeputyRaw $legislatura $id");
        var deputiesString = await _searchDeputyRepository.GetDeputy(legislatura, id);
        return deputiesString;    
    }

    public async Task<string> GetDeputyExpensesRaw(int year, int month ,int id)
    {
        _logger.Information("GetDeputyExpensesRaw $year $id");
        var deputyExpenses = await _searchDeputyRepository.GetAllExpenses(year, month, id);
        return deputyExpenses;       
    }

    public async Task<string> GetDeputyWorkPresenceRaw(int year, int month, int id)
    {
        _logger.Information("GetDeputyWorkPresenceRaw $year $id");
        var deputyWorkPresense = await _searchDeputyRepository.GetAllWorkPresence(year, month, id);
        return deputyWorkPresense;       
    }

    public async Task RefreshDatabase(int legislatura, int year)
    {
        DeputiesListNewApi deputiesListNewApi = await _searchDeputyRepository.GetAllDeputiesNewApi(legislatura);
        foreach (var deputy in deputiesListNewApi.DeputiesNewApi)
        {
            var id = Convert.ToInt32(deputy.Id);
            var deputyDetailNewApi = await _searchDeputyRepository.GetDeputyDetailNewApi(legislatura, id);
            await _nonRelationalDatabase.CheckAndUpdate(deputyDetailNewApi, deputyDetailNewApi.IdEntity.ToString());
            var currentMonth = DateTime.Now.Year == year ? DateTime.Now.Month : 12;
            for (int month = 1; month <= currentMonth; month++)
            {
                // var deputyExpenses = await _searchDeputyRepository.GetAllExpenses(year, month, id);
                // await _nonRelationalDatabase.Insert(deputyExpenses);
            }
        }
        
        var deputiesListOldApi = await _searchDeputyRepository.GetAllDeputiesOldApi(legislatura);
        foreach (var deputy in deputiesListOldApi.DeputiesOldApi)
        {
            var id = Convert.ToInt32(deputy.IdeCadastro);
            var deputyDetailOldApi = await _searchDeputyRepository.GetDeputyDetailOldApi(legislatura, id);
            await _nonRelationalDatabase.CheckAndUpdate(deputyDetailOldApi, deputyDetailOldApi.IdEntity.ToString());
            var currentMonth = DateTime.Now.Year == year ? DateTime.Now.Month : 12;
            for (int month = 1; month <= currentMonth; month++)
            {
                //var deputyWorkPresence = await _searchDeputyRepository.GetAllWorkPresence(year, month, id);
                // await _nonRelationalDatabase.Insert(deputyWorkPresence);
            }
        }
    }
}