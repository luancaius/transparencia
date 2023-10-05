namespace Repositories.Interfaces;

public interface ISearchDeputyRepository
{
    Task<string> GetAllDeputiesRaw(int legislatura);
    Task<string> GetDeputy(int legislatura, int id);
    Task<string> GetAllExpenses(int year, int month, int id);
    Task<string> GetAllWorkPresence(int year, int month, int id);
}