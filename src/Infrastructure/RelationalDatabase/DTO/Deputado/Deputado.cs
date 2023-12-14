using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace RelationalDatabase.DTO.Deputado;

[Table("Deputado", Schema = "congresso")]
public class Deputado : BaseEntity
{
        public string? Uri { get; set; }
        public string Nome { get; set; }
        public string NomeEleitoral { get; set; }
        public string NomeCivil { get; set; }
        public string NomeParlamentarAtual { get; set; }
        public string SiglaPartido { get; set; }
        public string? UriPartido { get; set; }
        public string SiglaUf { get; set; }
        public int Legislatura { get; set; }
        public string? UrlFoto { get; set; }
        public string Email { get; set; }
        public string Sexo { get; set; }
        public string UfRepresentacaoAtual { get; set; }
        public string SituacaoNaLegislaturaAtual { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
        public DateTime? DataFalecimento { get; set; }
        public string UfNascimento { get; set; }
        public string MunicipioNascimento { get; set; }
        public string? Escolaridade { get; set; }
        public string? UrlWebsite { get; set; }
        public string? RedeSocial { get; set; }
        public DateTime? Data { get; set; }
        public string Situacao { get; set; }
        public string CondicaoEleitoral { get; set; }
        
        public virtual ICollection<Comissao> Comissoes { get; set; }
        public virtual ICollection<DeputyExpenses> Expenses { get; set; }
        public virtual ICollection<DeputyWorkPresence> WorkPresences { get; set; }
}