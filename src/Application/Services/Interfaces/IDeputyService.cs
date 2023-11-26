using Services.DTO;

namespace Services.Interfaces;

public interface IDeputyService
{
    Task<DeputiesListDto> GetDeputiesListExternalApi(int legislatura);
    Task<DeputiesDetailListDto> GetDeputiesDetailListExternalApi(int legislatura);
    Task RefreshNewApi(int year);
    Task RefreshOldApi(int year);
    Task RefreshAllMongoDb(int year);
    Task RefreshRelationalDbFromNonRelationalDb(int year);
}