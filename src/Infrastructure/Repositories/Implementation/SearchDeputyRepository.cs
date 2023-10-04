using Repositories.Interfaces;

namespace Repositories.Implementation;

public class SearchDeputyRepository : ISearchDeputyRepository
{
    public string GetAllDeputies(int legislatura)
    {
        // call rest api get all deputies
        
        // call soap api get all deputies
        
        // save on mongo unique information
        throw new NotImplementedException();
    }

    public string GetDeputy(int legislatura, int id)
    {
        throw new NotImplementedException();
    }

    public string GetAllExpenses(int year, int month, int id)
    {
        throw new NotImplementedException();
    }

    public string GetAllWorkPresence(int year, int month, int id)
    {
        throw new NotImplementedException();
    }
}