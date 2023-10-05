using System.Linq.Expressions;
using Entities.DomainEntities;

namespace Services.Interfaces;

public interface IDeputyService
{
    Task<String> GetAllDeputyRaw(int legislatura);
    Task<String> GetDeputyRaw(int legislatura, int id);
    Task<String> GetDeputyExpensesRaw(int year, int id);
    Task<String> GetDeputyWorkPresenceRaw(int year, int id);
}