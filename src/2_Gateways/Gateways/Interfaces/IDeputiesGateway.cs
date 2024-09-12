using DTO.Layer_2_3;

namespace Gateways.Interfaces;

public interface IDeputiesGateway
{
    Task<List<DeputyListItem>> GetDeputiesList(int legislatura);
    Task<DeputyDetailInfo> GetDeputyDetailInfo(int id);
}