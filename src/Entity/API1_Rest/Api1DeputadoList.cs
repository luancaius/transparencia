using Newtonsoft.Json;

namespace Entity.API1_Rest;

public class Api1DeputadoList
{
    [JsonProperty("dados")]
    public List<DeputadoItem> DeputadoList { get; set; }
}