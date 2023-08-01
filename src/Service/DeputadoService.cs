using Entity.Congresso;

namespace Service;

public class DeputadoService : ApiService
{
    private String baseUrl = "https://dadosabertos.camara.leg.br/api/v2";
    public async Task GetAll()
    {
        String apiUrl = "/deputados";

        Deputado[] deputados = await GetAsync<Deputado[]>(baseUrl+apiUrl);
        String test = deputados[0].Nome;
    }
}