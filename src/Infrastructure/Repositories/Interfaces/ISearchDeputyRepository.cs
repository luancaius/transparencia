using Repositories.DTO;

namespace Repositories.Interfaces;

public interface ISearchDeputyRepository
{
    Task<DeputiesListOldApi> GetAllDeputiesOldApi(int legislatura);
    Task<DeputiesListNewApi> GetAllDeputiesNewApi(int legislatura);

    Task<string> GetDeputy(int legislatura, int id);
    Task<string> GetAllExpenses(int year, int month, int id);
    Task<string> GetAllWorkPresence(int year, int month, int id);
}