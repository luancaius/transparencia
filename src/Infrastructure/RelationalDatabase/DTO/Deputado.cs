using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RelationalDatabase.DTO;

[Table("Deputado", Schema = "Congresso")]
public class Deputado
{
    public class DeputyDetail
    {
        [Key]
        public int IdDeputy { get; set; }
        public string Uri { get; set; }
        public string Nome { get; set; }
        public string SiglaPartido { get; set; }
        public string UriPartido { get; set; }
        public string SiglaUf { get; set; }
        public int IdLegislatura { get; set; }
        public string UrlFoto { get; set; }
        public string Email { get; set; }

        public int IdeCadastro { get; set; }
        public string NomeCivil { get; set; }
        public string NomeParlamentarAtual { get; set; }
        public string Sexo { get; set; }
        public string UfRepresentacaoAtual { get; set; }
        public string SituacaoNaLegislaturaAtual { get; set; }
        public int IdParlamentarDeprecated { get; set; }

        // Navigation properties
        [ForeignKey("PartidoAtualId")]
        public virtual PartidoAtual PartidoAtual { get; set; }
        [ForeignKey("GabineteId")]
        public virtual Gabinete Gabinete { get; set; }
        public virtual ICollection<Comissao> Comissoes { get; set; }
    }
}