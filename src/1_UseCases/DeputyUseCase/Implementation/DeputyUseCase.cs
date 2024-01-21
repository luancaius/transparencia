using DeputyUseCase.DTO;
using DeputyUseCase.Interfaces;
using Entities.ValueObject;

namespace DeputyUseCase.Implementation;

public class DeputyUseCase : IDeputyUseCase
{
    public Task GetAndStoreDeputiesDetailsInfo(int year)
    {
        // TODO: get all deputies for the legislatura using new api call throw gateways
        var legislaturaVO = Legislatura.CriarLegislaturaPorAno(year);
        List<DeputyListItem> deputiesListItem = await _gateway.GetDeputiesList(legislaturaVO.Numero);
        // TODO: for each deputy, get deputy details using new api call throw gateways
        // TODO: for each detail info, save on mongodb
        // TODO: after calling all deputies, save on relational db
        throw new NotImplementedException();
    }

    public Task GetAndStoreDeputiesExpenses(int year)
    {
        throw new NotImplementedException();
    }
}