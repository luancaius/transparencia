namespace Repositories.DTO.NonRelational;

public class DeputyDetailMongo
{
    public int Id { get; set; }
    public string? NomeCivil { get; set; }
    public string Cpf { get; set; }
    public string? Sexo { get; set; }
    public DateTime? DataNascimento { get; set; } 
    public DateTime? DataFalecimento { get; set; } 
    public string? UfNascimento { get; set; }
    public string MunicipioNascimento { get; set; }
    public string? Escolaridade { get; set; }
    public string UrlWebsite { get; set; }
    public List<string> RedeSocial { get; set; }
    public string SiglaPartido { get; set; }
    public string SiglaUf { get; set; }
    public int Legislatura { get; set; }
    public string UrlFoto { get; set; }
    public string Email { get; set; }
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