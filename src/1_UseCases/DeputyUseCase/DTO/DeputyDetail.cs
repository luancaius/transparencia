public class DeputyDetail
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
}

    
