using Newtonsoft.Json.Linq;

namespace Repositories.DTO.NewApi.GetAll;

public class DeputiesListNewApi
{
    public List<DeputyNewApi> DeputyListNewApi { get; set; }

    public DeputiesListNewApi(String rawDeputiesList)
    {
        JObject root = JObject.Parse(rawDeputiesList);
        JArray deputadosArray = (JArray)root["dados"];

        DeputyListNewApi = new List<DeputyNewApi>();
        foreach (JToken deputadoToken in deputadosArray)
        {
            var temp = new DeputyNewApi();
            temp.Id = deputadoToken["id"].ToString();
            temp.Uri = deputadoToken["uri"].ToString();
            temp.Nome = deputadoToken["nome"].ToString();
            temp.SiglaPartido = deputadoToken["siglaPartido"].ToString();
            temp.UriPartido = deputadoToken["uriPartido"].ToString();
            temp.SiglaUf = deputadoToken["siglaUf"].ToString();
            temp.IdLegislatura = deputadoToken["idLegislatura"].Value<int>();
            temp.UrlFoto = deputadoToken["urlFoto"].ToString();
            temp.Email = deputadoToken["email"].ToString();
            DeputyListNewApi.Add(temp); 
        }
    }
}