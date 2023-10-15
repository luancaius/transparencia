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

    public async Task<DeputiesList> GetDeputiesListExternalApi(int legislatura)
    {
        _logger.Information("GetDeputiesListExternalApi");
        var deputiesListNewApi = await _searchDeputyRepository.GetAllDeputiesNewApi(legislatura);
        
        var deputiesListOldApi = await _searchDeputyRepository.GetAllDeputiesOldApi(legislatura);
        
        var deputiesList = new DeputiesList(deputiesListOldApi, deputiesListNewApi);
        return deputiesList;
    }

    public async Task<string> GetDeputyRaw(int legislatura, int id)
    {
        _logger.Information("GetDeputyRaw");
        var deputiesString = await _searchDeputyRepository.GetDeputy(legislatura, id);
        return deputiesString;    }

    public async Task<string> GetDeputyExpensesRaw(int year, int id)
    {
        throw new NotImplementedException();
    }

    public async Task<string> GetDeputyWorkPresenceRaw(int year, int id)
    {
        throw new NotImplementedException();
    }
}