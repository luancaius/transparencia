using Logging;
using Repositories.Interfaces;
using Serilog;
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

    public async Task<string> GetAllDeputyRaw(int legislatura)
    {
        _logger.Information("GetAllDeputyRaw");
        var deputiesString = await _searchDeputyRepository.GetAllDeputiesRaw(legislatura);
        return deputiesString;
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