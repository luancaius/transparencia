using Entity.Congresso;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Service;

public static class DeputadoMapper
{
    public static List<Deputado> map(string deputadosRaw)
    {
        JObject jsonObject = JObject.Parse(deputadosRaw);
        string dados = jsonObject["dados"].ToString();
        List<Deputado> response = JsonConvert.DeserializeObject<List<Deputado>>(dados);

        return response;
    }
    
    public static Deputado mapById(string deputadoRaw)
    {
        JObject jsonObject = JObject.Parse(deputadoRaw);
        string dados = jsonObject["dados"].ToString();
        Deputado response = JsonConvert.DeserializeObject<Deputado>(dados);

        return response;
    }
}