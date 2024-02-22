using NonRelationalDatabase.Interfaces;
using Repositories.DTO.NonRelational;
using Repositories.DTO.Relational;
using Repositories.Interfaces;
using Serilog;

namespace Repositories.Implementation;

public class Repository : IRepository
{
    public INonRelationalDatabase _nonRelationalDatabase;
    public IRelationalDatabase _relationalDatabase;
    private readonly ILogger _logger;

    public Repository(INonRelationalDatabase nonRelationalDatabase,  ILogger logger)
    {
        _nonRelationalDatabase = nonRelationalDatabase;
        _logger = logger.ForContext<Repository>();
    }
    public async Task SaveNonRelationalData(DeputyDetailMongo deputyDetailRepo)
    {
        try
        {
            await _nonRelationalDatabase.Upsert(deputyDetailRepo);
        }
        catch (Exception e)
        {
            _logger.Error("Error on saving non relational data", e);
        }
    }

    public Task SaveRelationalData(DeputyDetailRepoRelational deputyDetailRepoRelational)
    {
        var personRelationalData = PersonRepoRelational.ConvertFrom(deputyDetailRepoRelational);
        // convert to deputy relational data
        //var deputyRelationalData = deputyDetailRepoRelational.ConvertToDeputyRelationalData();
        // save person relational data
        
        // save deputy relational data
        return Task.CompletedTask;
    }
}
