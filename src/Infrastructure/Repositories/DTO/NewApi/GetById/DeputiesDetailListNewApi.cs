using Newtonsoft.Json.Linq;
using Repositories.DTO.NewApi.GetAll;

namespace Repositories.DTO.NewApi.GetById;

public class DeputiesDetailListNewApi
{
    public List<DeputyDetailNewApi> DeputiesDetailNewApi { get; set; }

    public DeputiesDetailListNewApi(String rawDeputiesList)
    {
        JObject root = JObject.Parse(rawDeputiesList);
        JArray deputadosArray = (JArray)root["dados"];

        DeputiesDetailNewApi = new List<DeputyDetailNewApi>();
        foreach (JToken deputadoToken in deputadosArray)
        {
            var temp = new DeputyDetailNewApi();
            temp.Id = deputadoToken["id"].ToString();
            temp.Uri = deputadoToken["uri"].ToString();
            temp.Nome = deputadoToken["nome"].ToString();
            temp.SiglaPartido = deputadoToken["siglaPartido"].ToString();
            temp.UriPartido = deputadoToken["uriPartido"].ToString();
            temp.SiglaUf = deputadoToken["siglaUf"].ToString();
            temp.IdLegislatura = deputadoToken["idLegislatura"].Value<int>();
            temp.UrlFoto = deputadoToken["urlFoto"].ToString();
            temp.Email = deputadoToken["email"].ToString();
            DeputiesDetailNewApi.Add(temp); 
        }
    }
}