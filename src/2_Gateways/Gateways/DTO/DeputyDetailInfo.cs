using System.Globalization;
using Newtonsoft.Json;

namespace Gateways.DTO;

public class DeputyDetailInfo
{
    public int IdDeputy { get; }
    public string Uri { get; }
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

    // Properties from 'ultimoStatus'
    public string Nome { get; }
    public string SiglaPartido { get; }
    public string UriPartido { get; }
    public string SiglaUf { get; }
    public int IdLegislatura { get; }
    public string UrlFoto { get; }
    public string Email { get; }
    public DateTime? Data { get; set; }
    public string NomeEleitoral { get; set; }
    public string Situacao { get; set; }
    public string CondicaoEleitoral { get; set; }
    public Gabinete GabineteInfo { get; set; }

    public class Gabinete
    {
        public string Nome { get; set; }
        public string Predio { get; set; }
        public string Sala { get; set; }
        public string Andar { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
    }

    public DeputyDetailInfo(string deputyDetailRaw)
    {
        try
        {
            dynamic jsonObject = JsonConvert.DeserializeObject(deputyDetailRaw);
            dynamic dados = jsonObject.dados;
            dynamic ultimoStatus = dados.ultimoStatus;

            IdDeputy = dados.id;
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

            Nome = ultimoStatus.nome;
            SiglaPartido = ultimoStatus.siglaPartido;
            UriPartido = ultimoStatus.uriPartido;
            SiglaUf = ultimoStatus.siglaUf;
            IdLegislatura = ultimoStatus.idLegislatura;
            UrlFoto = ultimoStatus.urlFoto;
            Email = ultimoStatus.email;
            Data = ParseNullableDateTime(ultimoStatus.data.ToString());
            NomeEleitoral = ultimoStatus.nomeEleitoral;
            Situacao = ultimoStatus.situacao;
            CondicaoEleitoral = ultimoStatus.condicaoEleitoral;
            GabineteInfo = new Gabinete
            {
                Nome = ultimoStatus.gabinete.nome,
                Predio = ultimoStatus.gabinete.predio,
                Sala = ultimoStatus.gabinete.sala,
                Andar = ultimoStatus.gabinete.andar,
                Telefone = ultimoStatus.gabinete.telefone,
                Email = ultimoStatus.gabinete.email
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Gateways.DTO.DeputyDetailInfo Error parsing JSON: {ex.Message}");
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