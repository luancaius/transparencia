using Repositories.Interfaces;
using Serilog;
using Services.DTO;
using Services.Interfaces;

namespace Services.Service;

public class DeputyService : IDeputyService
{
    private readonly IDeputyRepository _deputyRepository;
    private readonly ISearchDeputyRepository _searchDeputyRepository;
    private readonly ILogger _logger;

    public DeputyService(IDeputyRepository deputyRepository, ISearchDeputyRepository searchDeputyRepository, ILogger logger)
    {
        _deputyRepository = deputyRepository;
        _searchDeputyRepository = searchDeputyRepository;
        _logger = logger.ForContext<DeputyService>();
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
        var deputiesListDto = await GetDeputiesListExternalApi(legislatura);
        var deputies = deputiesListDto.Deputies;
        foreach (var deputy in deputies)
        {
            var deputyDetailOldApi = await _searchDeputyRepository.GetDeputyDetailOldApi(legislatura, deputy.IdeCadastro);
            var deputyDetailNewApi = await _searchDeputyRepository.GetDeputyDetailNewApi(legislatura, deputy.IdeCadastro);
            var deputyDetailUnified = new DeputyDetailDto(deputyDetailOldApi, deputyDetailNewApi);
        }

        return null;

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
}