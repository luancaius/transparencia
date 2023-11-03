using Services.DTO;

namespace Services.Interfaces;

public interface IDeputyService
{
    Task<DeputiesListDto> GetDeputiesListExternalApi(int legislatura);
    Task<DeputiesDetailListDto> GetDeputiesDetailListExternalApi(int legislatura);
    Task<String> GetDeputyRaw(int legislatura, int id);
    Task<String> GetDeputyExpensesRaw(int year, int month, int id);
    Task<String> GetDeputyWorkPresenceRaw(int year, int month, int id);
    Task RefreshDatabase(int legislatura, int year);
}