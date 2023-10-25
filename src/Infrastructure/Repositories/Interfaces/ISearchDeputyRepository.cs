using Repositories.DTO;
using Repositories.DTO.NewApi;
using Repositories.DTO.NewApi.GetAll;
using Repositories.DTO.OldApi.GetAll;

namespace Repositories.Interfaces;

public interface ISearchDeputyRepository
{
    Task<DeputiesListOldApi> GetAllDeputiesOldApi(int legislatura);
    Task<DeputiesListNewApi> GetAllDeputiesNewApi(int legislatura);

    Task<DeputiesListOldApi> GetDeputiesDetailOldApi(int legislatura, int id);
    Task<DeputiesListNewApi> GetAllDeputiesDetailNewApi(int legislatura, int id);
    Task<string> GetDeputy(int legislatura, int id);
    Task<string> GetAllExpenses(int year, int month, int id);
    Task<string> GetAllWorkPresence(int year, int month, int id);
}