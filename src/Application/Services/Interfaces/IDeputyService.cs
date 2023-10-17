using System.Linq.Expressions;
using Entities.DomainEntities;
using Services.DTO;

namespace Services.Interfaces;

public interface IDeputyService
{
    Task<DeputiesList> GetDeputiesListExternalApi(int legislatura);
    Task<String> GetDeputyRaw(int legislatura, int id);
    Task<String> GetDeputyExpensesRaw(int year, int month, int id);
    Task<String> GetDeputyWorkPresenceRaw(int year, int month, int id);
}