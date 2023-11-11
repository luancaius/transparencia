using Services.DTO;

namespace Services.Interfaces;

public interface IDeputyService
{
    Task<DeputiesListDto> GetDeputiesListExternalApi(int legislatura);
    Task<DeputiesDetailListDto> GetDeputiesDetailListExternalApi(int legislatura);
    Task RefreshDatabase(int legislatura, int year);
}