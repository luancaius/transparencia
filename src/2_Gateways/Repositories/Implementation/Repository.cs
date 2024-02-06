using NonRelationalDatabase.Interfaces;
using Repositories.DTO.NonRelational;
using Repositories.Interfaces;

namespace Repositories.Implementation;

public class Repository : IRepository
{
    public INonRelationalDatabase _nonRelationalDatabase;
    public Repository(INonRelationalDatabase nonRelationalDatabase)
    {
        _nonRelationalDatabase = nonRelationalDatabase;
    }
    public async Task SaveNonRelationalData(DeputyDetailRepo deputyDetailRepo)
    {
        await _nonRelationalDatabase.Insert(deputyDetailRepo);
    }

    public Task SaveRelationalData(DeputyDetailRepo deputyDetail)
    {
        // convert to person relational data
        // convert to deputy relational data
        // save relational data

        return Task.CompletedTask; // Add return statement
    }
}
