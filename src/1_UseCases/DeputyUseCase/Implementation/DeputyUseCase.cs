using DeputyUseCase.DTO;
using DeputyUseCase.Interfaces;
using Entities.ValueObject;
using Gateways.Interfaces;

namespace DeputyUseCase.Implementation;

public class DeputyUseCase : IDeputyUseCase
{
    private readonly IDeputiesGateway _deputiesGateway;

    public DeputyUseCase(IDeputiesGateway deputiesGateway)
    {
        _deputiesGateway = deputiesGateway;
    }
    public async Task GetAndStoreDeputiesDetailsInfo(int year)
    {
        var legislaturaVO = Legislatura.CriarLegislaturaPorAno(year);
        var deputiesListItem = await _deputiesGateway.GetDeputiesList(legislaturaVO.Numero);
        foreach (var deputyListItem in deputiesListItem)
        {
            var deputyDetailInfo = await _deputiesGateway.GetDeputyDetailInfo(deputyListItem.Id);
            
        }
        // TODO: after calling all deputies, save on relational db
        throw new NotImplementedException();
    }

    public Task GetAndStoreDeputiesExpenses(int year)
    {
        throw new NotImplementedException();
    }
}