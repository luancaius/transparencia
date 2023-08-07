using Newtonsoft.Json;

namespace Service.DTO.API1;

public class Api1DeputadoDadosDto
{
    [JsonProperty("dados")]
    public Api1DeputadoDto DeputadoApi1 { get; set; }
}