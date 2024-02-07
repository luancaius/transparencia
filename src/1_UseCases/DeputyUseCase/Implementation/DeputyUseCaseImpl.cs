using DeputyUseCase.DTO;
using DeputyUseCase.Interfaces;
using Entities.ValueObject;
using Gateways.DTO;
using Gateways.Interfaces;
using Repositories.DTO.NonRelational;
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
            var deputyDetailRepo = deputyDetailInfo.ConvertToRepo();
            await _repository.SaveNonRelationalData(deputyDetailRepo);
            await _repository.SaveRelationalData(deputyDetailRepo);
        }
    }

    public Task GetAndStoreDeputiesExpenses(int year)
    {
        // get all deputies 
        // foreach deputy id, make the expense call for the month of the year
        // save the raw value on mongo
        // save relational data
        throw new NotImplementedException();
    }
}