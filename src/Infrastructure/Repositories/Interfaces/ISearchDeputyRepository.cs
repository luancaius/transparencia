using Repositories.DTO.NewApi.Expense;
using Repositories.DTO.NewApi.GetAll;
using Repositories.DTO.OldApi.GetAll;
using Repositories.DTO.OldApi.GetById;
using Repositories.DTO.OldApi.WorkPresence;

namespace Repositories.Interfaces;

public interface ISearchDeputyRepository
{
    Task<DeputiesListOldApi> GetAllDeputiesOldApi(int legislatura);
    Task<DeputiesListNewApi> GetAllDeputiesNewApi(int legislatura);

    Task<DeputyDetailOldApi> GetDeputyDetailOldApi(int legislatura, int id);
    Task<DeputyDetailNewApi> GetDeputyDetailNewApi(int legislatura, int id);
    Task<DeputyExpense> GetDeputyExpense(int year, int month, int id);
    Task<DeputyWorkPresence> GetDeputyWorkPresence(int year, int month, int id);
}