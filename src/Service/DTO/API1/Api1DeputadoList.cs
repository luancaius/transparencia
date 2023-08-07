using Newtonsoft.Json;

namespace Service.DTO.API1;

public class Api1DeputadoList
{
    [JsonProperty("dados")]
    public List<DeputadoItem> DeputadoList { get; set; }
}