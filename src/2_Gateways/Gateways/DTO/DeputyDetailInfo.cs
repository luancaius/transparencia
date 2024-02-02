using System.Globalization;
using Newtonsoft.Json;

namespace Gateways.DTO;

public class DeputyDetailInfo
{
    public int IdDeputyAPI { get; set; }
    public string Uri { get; set; }
    public string NomeCivil { get; set; }
    public string Cpf { get; set; }
    public string Sexo { get; set; }
    public DateTime DataNascimento { get; set; }
    public DateTime? DataFalecimento { get; set; }
    public string UfNascimento { get; set; }
    public string MunicipioNascimento { get; set; }
    public string Escolaridade { get; set; }
    public string UrlWebsite { get; set; }
    public List<string> RedeSocial { get; set; }
    public string Nome { get; set; }
    public string SiglaPartido { get; set; }
    public string UriPartido { get; set; }
    public string SiglaUf { get; set; }
    public int Legislatura { get; set; }
    public string UrlFoto { get; set; }
    public string Email { get; set; }
    public string NomeEleitoral { get; set; }
    public string Situacao { get; set; }
    public string CondicaoEleitoral { get; set; }

    public DeputyDetailInfo(string deputyRaw)
    {
        try
        {
            dynamic jsonObject = JsonConvert.DeserializeObject(deputyRaw);
            dynamic dados = jsonObject.dados;
            dynamic ultimoStatus = dados.ultimoStatus;

            Uri = dados.uri;
            NomeCivil = dados.nomeCivil;
            Cpf = dados.cpf;
            Sexo = dados.sexo;
            DataNascimento = ParseNullableDateTime(dados.dataNascimento.ToString());
            DataFalecimento = ParseNullableDateTime(dados.dataFalecimento.ToString());
            UfNascimento = dados.ufNascimento;
            MunicipioNascimento = dados.municipioNascimento;
            Escolaridade = dados.escolaridade;
            UrlWebsite = dados.urlWebsite;
            RedeSocial = dados.redeSocial.ToObject<List<string>>(); 
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error parsing JSON: {ex.Message}");
        }
    }

    private DateTime? ParseNullableDateTime(string dateTimeString)
    {
        if (DateTime.TryParse(dateTimeString, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out DateTime result))
        {
            return result;
        }
        return null;
    }
}