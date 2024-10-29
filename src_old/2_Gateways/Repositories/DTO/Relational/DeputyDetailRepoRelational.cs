using RelationalDatabase.DTO;
using RelationalDatabase.DTO.Deputado;

namespace Repositories.DTO.Relational;

public class DeputyDetailRepoRelational
{
    public int IdDeputy { get; set; }
    public string NomeCivil { get; set; }
    public string Cpf { get; set; }
    public string Sexo { get; set; }
    public DateTime DataNascimento { get; set; } 
    public string UfNascimento { get; set; }
    public string MunicipioNascimento { get; set; }
    public string Escolaridade { get; set; }

    public string Nome { get; set; }
    public string SiglaPartido { get; set; }
    public string SiglaUf { get; set; }
    public int IdLegislatura { get; set; }
    public string UrlFoto { get; set; }
    public string Email { get; set; }
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

    public Person ConvertToPersonDB()
    {   
        return new Person()
        {
            FullName = this.NomeCivil,
            Cpf = this.Cpf,
            Gender = this.Sexo,
            DateOfBirth = this.DataNascimento,
            Email = this.Email,
            EstadoNascimento = this.UfNascimento,
            MunicipioNascimento = this.MunicipioNascimento,
            Escolaridade = this.Escolaridade
        };
    }

    public Deputado ConvertToDeputyDB()
    {
        return new Deputado()
        { 
            NomeEleitoral = this.NomeEleitoral,
            UfNascimento = this.UfNascimento,
            Email = this.Email,
            SiglaPartido = this.SiglaPartido,
            UfRepresentacaoAtual = this.SiglaUf,
            IdApi = this.IdDeputy.ToString()
        };
    }
}