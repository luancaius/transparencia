using Services.DTO.Deputy;

namespace Services.Interfaces;

public interface IDeputyService
{
    Task<DeputiesListDto> GetDeputiesListExternalApi(int legislatura);
    Task<DeputiesDetailListDto> GetDeputiesDetailListExternalApi(int legislatura);
    Task RefreshDeputyDetails(int year);
    Task RefreshNewApi(int year);
    Task RefreshOldApi(int year);
    Task RefreshAllMongoDb(int year);
    Task RefreshRelationalDbFromNonRelationalDb();
    Task RefreshDeputyDetailRelationalDb();
    Task DownloadReceipts(string url);
}