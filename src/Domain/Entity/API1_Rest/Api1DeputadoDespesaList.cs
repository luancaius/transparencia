using Newtonsoft.Json;

namespace Entity.API1_Rest;

public class Api1DeputadoDespesaList
{
    [JsonProperty("dados")]
    public List<Api1DeputadoDespesa> DeputadoDespesaList { get; set; }
}