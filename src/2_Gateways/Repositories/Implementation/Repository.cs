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
    public Task SaveNonRelationalData(DeputyDetailRepo deputyDetail)
    {
        throw new NotImplementedException();
    }
}
