using DeputyUseCase.Interfaces;
using DeputyUseCase.Mapper;
using Entities.ValueObject;
using Gateways.Interfaces;
using Repositories.Interfaces;

namespace DeputyUseCase.Implementation;

public class DeputyUseCaseImpl : IDeputyUseCase
{
    private readonly IDeputiesGateway _deputiesGateway;
    private readonly IRepository _repository;

    public DeputyUseCaseImpl(IDeputiesGateway deputiesGateway, IRepository repository)
    {
        _deputiesGateway = deputiesGateway;
        _repository = repository;
    }
    public async Task GetAndStoreDeputiesDetailsInfo(int year)
    {
        var legislaturaVO = Legislatura.CriarLegislatura(year);
        var deputiesListItem = await _deputiesGateway.GetDeputiesList(legislaturaVO.Numero);
        foreach (var deputyListItem in deputiesListItem)
        {
            var deputyDetailInfo = await _deputiesGateway.GetDeputyDetailInfo(deputyListItem.IdDeputyAPI);
            var deputyDomain = ConvertToDomain.Deputy(deputyDetailInfo);
            var deputyDetailRepo = ConvertFromDomain.DeputyDetailRepo(deputyDomain);
            await _repository.SaveNonRelationalData(deputyDetailRepo);
            var deputyDetailRepoRelational = ConvertFromDomain.DeputyDetailRepoRelational(deputyDomain);
            await _repository.SaveRelationalData(deputyDetailRepoRelational);
        }
    }

    public Task GetAndStoreDeputiesExpenses(int year)
    {
        // get all deputies from non relational repo
        // foreach deputy id, make the expense call for the month of the year
        // save the raw value on mongo
        // save relational data
        throw new NotImplementedException();
    }
}