using Repositories.DTO.NewApi.GetAll;
using Repositories.DTO.OldApi.GetAll;
using Repositories.DTO.OldApi.GetById;

namespace Repositories.Interfaces;

public interface ISearchDeputyRepository
{
    Task<DeputiesListOldApi> GetAllDeputiesOldApi(int legislatura);
    Task<DeputiesListNewApi> GetAllDeputiesNewApi(int legislatura);

    Task<DeputyDetailOldApi> GetDeputyDetailOldApi(int legislatura, int id);
    Task<DeputyDetailNewApi> GetDeputyDetailNewApi(int legislatura, int id);
    Task<string> GetDeputy(int legislatura, int id);
    Task<string> GetAllExpenses(int year, int month, int id);
    Task<string> GetAllWorkPresence(int year, int month, int id);
}