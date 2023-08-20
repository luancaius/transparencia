using Newtonsoft.Json;

namespace Entity.API1_Rest;

public class Api1DeputadoDadosDto
{
    [JsonProperty("dados")]
    public Api1DeputadoDto DeputadoApi1 { get; set; }
}