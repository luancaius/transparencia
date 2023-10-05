using Repositories.Interfaces;

namespace Repositories.Implementation;

public class SearchDeputyRepository : ISearchDeputyRepository
{
    public async Task<string> GetAllDeputiesRaw(int legislatura)
    {
        // call rest api get all deputies
        
        // call soap api get all deputies
        
        // save on mongo unique information
        throw new NotImplementedException();
    }

    public Task<string> GetDeputy(int legislatura, int id)
    {
        throw new NotImplementedException();
    }

    public Task<string> GetAllExpenses(int year, int month, int id)
    {
        throw new NotImplementedException();
    }

    public Task<string> GetAllWorkPresence(int year, int month, int id)
    {
        throw new NotImplementedException();
    }
}