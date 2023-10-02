using Newtonsoft.Json;

namespace Entity.API1_Rest;

public class Api1DeputadoList
{
    [JsonProperty("dados")]
    public List<Api1DeputadoItem> DeputadoList { get; set; }
}