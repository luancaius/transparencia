using Newtonsoft.Json.Linq;

namespace DTO.Layer_2_3;

public class DeputyListItem
{
    public int IdDeputyAPI { get; set; }
    public string Nome { get; set; } 
    public string SiglaPartido { get; set; }
    public int Legislatura { get; set; }

    public static List<DeputyListItem> MapFromString(String deputyRaw)
    {
        JObject root = JObject.Parse(deputyRaw);
        JArray deputadosArray = (JArray)root["dados"];

        var deputiesList = new List<DeputyListItem>();
        foreach (JToken deputadoToken in deputadosArray)
        {
            var temp = new DeputyListItem();
            temp.IdDeputyAPI = Convert.ToInt32(deputadoToken["id"]);
            temp.Nome = deputadoToken["nome"].ToString();
            temp.SiglaPartido = deputadoToken["siglaPartido"].ToString();
            temp.Legislatura = deputadoToken["idLegislatura"].Value<int>();
            deputiesList.Add(temp); 
        }

        return deputiesList;
    }
}