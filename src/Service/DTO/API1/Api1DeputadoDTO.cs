using Newtonsoft.Json;

namespace Service.DTO.API1;

public class Api1DeputadoDadosDto
{
    [JsonProperty("dados")]
    public Api1DeputadoDto DeputadoApi1 { get; set; }
}
public class Api1DeputadoDto
{
    public long Id { get; set; }
    public string Uri { get; set; }
    public string NomeCivil { get; set; }
    public UltimoStatusDto UltimoStatus { get; set; }
    public string Cpf { get; set; }
    public string Sexo { get; set; }
    public string UrlWebsite { get; set; }
    public List<string> RedeSocial { get; set; }
    public string DataNascimento { get; set; }
    public string DataFalecimento { get; set; }
    public string UfNascimento { get; set; }
    public string MunicipioNascimento { get; set; }
    public string Escolaridade { get; set; }

    public class UltimoStatusDto
    {
        public long Id { get; set; }
        public string Uri { get; set; }
        public string Nome { get; set; }
        public string SiglaPartido { get; set; }
        public string UriPartido { get; set; }
        public string SiglaUf { get; set; }
        public int IdLegislatura { get; set; }
        public string UrlFoto { get; set; }
        public string Email { get; set; }
        public string Data { get; set; }
        public string NomeEleitoral { get; set; }
        public GabineteDto Gabinete { get; set; }
        public string Situacao { get; set; }
        public string CondicaoEleitoral { get; set; }
        public string DescricaoStatus { get; set; }
    }

    public class GabineteDto
    {
        public string Nome { get; set; }
        public string Predio { get; set; }
        public string Sala { get; set; }
        public string Andar { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
    }
}

public class DeputadoItem
{
    public int Id { get; set; }
    public string Uri { get; set; }
    public string Nome { get; set; }
    public string SiglaPartido { get; set; }
    public string UriPartido { get; set; }
    public string SiglaUf { get; set; }
    public int IdLegislatura { get; set; }
    public string UrlFoto { get; set; }
    public string Email { get; set; }
}

public class Api1DeputadoList
{
    [JsonProperty("dados")]
    public List<DeputadoItem> DeputadoList { get; set; }
}
