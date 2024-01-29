using Gateways.DTO;

namespace Gateways.Interfaces;

public interface IDeputiesGateway
{
    Task<List<DeputyListItem>> GetDeputiesList(int legislatura);
    Task<DeputyDetailInfo> GetDeputyDetailInfo(string id);
}