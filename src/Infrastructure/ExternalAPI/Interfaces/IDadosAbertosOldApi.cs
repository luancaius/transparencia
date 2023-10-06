namespace ExternalAPI.Interfaces;

public interface IDadosAbertosOldApi
{
    Task<string> GetAllDeputiesRaw(int legislatura);
    Task<string> GetDeputyRaw(int id);
    Task<string> GetDeputyWorkPresenceRaw(int year, int id);
}