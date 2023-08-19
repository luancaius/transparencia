using Newtonsoft.Json;

namespace Entity.API1;

public class Api1DeputadoDadosDto
{
    [JsonProperty("dados")]
    public Api1DeputadoDto DeputadoApi1 { get; set; }
}