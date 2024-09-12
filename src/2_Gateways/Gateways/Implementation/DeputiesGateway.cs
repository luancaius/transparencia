using DTO.Layer_2_3;
using ExternalAPI.Interfaces;
using Gateways.Interfaces;

namespace Gateways.Implementation;

public class DeputiesGateway : IDeputiesGateway
{
    private readonly IDadosAbertosNewApi _dadosAbertosNewApi;
    public DeputiesGateway(IDadosAbertosNewApi dadosAbertosNewApi)
    {
        _dadosAbertosNewApi = dadosAbertosNewApi;
    }
    public async Task<List<DeputyListItem>> GetDeputiesList(int legislatura)
    {
        var deputiesRaw = await _dadosAbertosNewApi.GetAllDeputiesRaw(legislatura);
        var deputiesList = DeputyListItem.MapFromString(deputiesRaw);
        return deputiesList;
    }

    public async Task<DeputyDetailInfo> GetDeputyDetailInfo(int id)
    {
        var deputiesRaw = await _dadosAbertosNewApi.GetDeputyRaw(id);
        var deputyDetail = new DeputyDetailInfo(deputiesRaw);
        return deputyDetail;    
    }
}