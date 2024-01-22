using DeputyUseCase.DTO;
using Gateways.Interfaces;

namespace Gateways.Implementation;

public class DeputiesGateway : IDeputiesGateway
{
    public Task<List<DeputyListItem>> GetDeputiesList(int legislatura)
    {
        throw new NotImplementedException();
    }
}