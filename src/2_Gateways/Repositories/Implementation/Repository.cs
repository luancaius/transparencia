using NonRelationalDatabase.Interfaces;
using Repositories.DTO.NonRelational;
using Repositories.Interfaces;

namespace Repositories.Implementation;

public class Repository : IRepository
{
    public INonRelationalDatabase _nonRelationalDatabase;
    public IRelationalDatabase _relationalDatabase;
    public Repository(INonRelationalDatabase nonRelationalDatabase, IRelationalDatabase relationalDatabase)
    {
        _nonRelationalDatabase = nonRelationalDatabase;
        _relationalDatabase = relationalDatabase;
    }
    public async Task SaveNonRelationalData(DeputyDetailRepo deputyDetailRepo)
    {
        await _nonRelationalDatabase.Insert(deputyDetailRepo);
    }

    public Task SaveRelationalData(DeputyDetailRepo deputyDetail)
    {
        // convert to person relational data
        var personRelationalData = deputyDetail.ConvertToPersonRelationalData();
        // convert to deputy relational data
        var deputyRelationalData = deputyDetail.ConvertToDeputyRelationalData();
        // save person relational data
        
        // save deputy relational data


        return Task.CompletedTask; // Add return statement
    }
}
