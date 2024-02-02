using DeputyUseCase.DTO;
using DeputyUseCase.Interfaces;
using Entities.ValueObject;
using Gateways.Interfaces;
using Repositories.Interfaces;

namespace DeputyUseCase.Implementation;

public class DeputyUseCaseImpl : IDeputyUseCase
{
    private readonly IDeputiesGateway _deputiesGateway;
    private readonly IRepository _repository;

    public DeputyUseCaseImpl(IDeputiesGateway deputiesGateway)
    {
        _deputiesGateway = deputiesGateway;
    }
    public async Task GetAndStoreDeputiesDetailsInfo(int year)
    {
        var legislaturaVO = Legislatura.CriarLegislaturaPorAno(year);
        var deputiesListItem = await _deputiesGateway.GetDeputiesList(legislaturaVO.Numero);
        foreach (var deputyListItem in deputiesListItem)
        {
            var deputyDetailInfo = await _deputiesGateway.GetDeputyDetailInfo(deputyListItem.IdDeputyAPI);
            
        }
    }

    public Task GetAndStoreDeputiesExpenses(int year)
    {
        throw new NotImplementedException();
    }
}