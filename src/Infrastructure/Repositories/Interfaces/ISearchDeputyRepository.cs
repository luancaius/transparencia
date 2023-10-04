namespace Repositories.Interfaces;

public interface ISearchDeputyRepository
{
    string GetAllDeputies(int legislatura);
    string GetDeputy(int legislatura, int id);
    string GetAllExpenses(int year, int month, int id);
    string GetAllWorkPresence(int year, int month, int id);
}