namespace ExternalAPI.Interfaces;

public interface IDadosAbertosNewApi
{
    Task<string> GetAllDeputiesRaw(int legislatura);
    Task<string> GetDeputyRaw(int id);
    Task<string> GetDeputyExpensesRaw(int year, int month, int id);
}