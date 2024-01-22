using DeputyUseCase.DTO;

namespace Gateways.Interfaces;

public interface IDeputiesGateway
{
    Task<List<DeputyListItem>> GetDeputiesList(int legislatura);
}