using Entity.Congresso;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Service;

public class DeputadoService : ApiService
{
    private String baseUrl = "https://dadosabertos.camara.leg.br/api/v2";
    public async Task GetAll()
    {
        String apiUrl = "/deputados";

        String deputados_raw = await GetAsync(baseUrl+apiUrl);

        List<Deputado> deputados = DeputadoMapper.map(deputados_raw);
    }
}

public static class DeputadoMapper
{
    public static List<Deputado> map(string deputadosRaw)
    {
        JObject jsonObject = JObject.Parse(deputadosRaw);
        string dados = jsonObject["dados"].ToString();
        List<Deputado> response = JsonConvert.DeserializeObject<List<Deputado>>(dados);

        return response;
    }
}
