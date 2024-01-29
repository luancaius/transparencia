using Gateways.DTO;
using Gateways.Interfaces;

namespace Gateways.Implementation;

public class DeputiesGateway : IDeputiesGateway
{
    public Task<List<DeputyListItem>> GetDeputiesList(int legislatura)
    {
        throw new NotImplementedException();
    }

    public Task<DeputyDetailInfo> GetDeputyDetailInfo(string id)
    {
        throw new NotImplementedException();
    }
}