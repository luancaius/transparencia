using Newtonsoft.Json;

public class DeputyDetailNewApi
{
    // Properties from 'dados'
    public int Id { get; set; }
    public string Uri { get; set; }
    public string NomeCivil { get; set; }
    public string Cpf { get; set; }
    public string Sexo { get; set; }
    public DateTime DataNascimento { get; set; }
    public string UfNascimento { get; set; }
    public string MunicipioNascimento { get; set; }
    public string Escolaridade { get; set; }

    // Properties from 'ultimoStatus'
    public string Nome { get; set; }
    public string SiglaPartido { get; set; }
    public string UriPartido { get; set; }
    public string SiglaUf { get; set; }
    public int IdLegislatura { get; set; }
    public string UrlFoto { get; set; }
    public string Email { get; set; }
    public DateTime Data { get; set; }
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

    public DeputyDetailNewApi(string deputyDetailString)
    {
        try
        {
            dynamic jsonObject = JsonConvert.DeserializeObject(deputyDetailString);
            dynamic dados = jsonObject.dados;
            dynamic ultimoStatus = dados.ultimoStatus;

            // Mapping 'dados' properties
            Id = dados.id;
            Uri = dados.uri;
            NomeCivil = dados.nomeCivil;
            Cpf = dados.cpf;
            Sexo = dados.sexo;
            DataNascimento = DateTime.Parse(dados.dataNascimento.ToString());
            UfNascimento = dados.ufNascimento;
            MunicipioNascimento = dados.municipioNascimento;
            Escolaridade = dados.escolaridade;

            // Mapping 'ultimoStatus' properties
            Nome = ultimoStatus.nome;
            SiglaPartido = ultimoStatus.siglaPartido;
            UriPartido = ultimoStatus.uriPartido;
            SiglaUf = ultimoStatus.siglaUf;
            IdLegislatura = ultimoStatus.idLegislatura;
            UrlFoto = ultimoStatus.urlFoto;
            Email = ultimoStatus.email;
            Data = DateTime.Parse(ultimoStatus.data.ToString());
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
            Console.WriteLine($"Error parsing JSON: {ex.Message}");
        }
    }
}
