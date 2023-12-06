using System.ComponentModel.DataAnnotations.Schema;

namespace RelationalDatabase.DTO.Deputado;

[Table("Deputado", Schema = "congresso")]
public class Deputado : BaseEntity
{
        public string Uri { get; set; }
        public string Nome { get; set; }
        public string SiglaPartido { get; set; }
        public string UriPartido { get; set; }
        public string SiglaUf { get; set; }
        public int Legislatura { get; set; }
        public string UrlFoto { get; set; }
        public string Email { get; set; }

        public string NomeCivil { get; set; }
        public string NomeParlamentarAtual { get; set; }
        public string Sexo { get; set; }
        public string UfRepresentacaoAtual { get; set; }
        public string SituacaoNaLegislaturaAtual { get; set; }

        // Navigation properties
        [ForeignKey("PartidoAtualId")]
        public virtual PartidoAtual PartidoAtual { get; set; }
        [ForeignKey("GabineteId")]
        public virtual Gabinete Gabinete { get; set; }
        public virtual ICollection<Comissao> Comissoes { get; set; }
        public virtual ICollection<DeputyExpenses> Expenses { get; set; }
}