using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RelationalDatabase.DTO.Deputado
{
    [Table("DeputyExpenses", Schema = "congresso")]
    public class DeputyExpenses : BaseEntity
    {
        public int Ano { get; set; }
        public int Mes { get; set; }
        public int IdDeputy { get; set; }
        public bool HasData { get; set; }
        [StringLength(100)]
        public string TipoDespesa { get; set; }
        public int CodDocumento { get; set; }
        [StringLength(100)]
        public string TipoDocumento { get; set; }
        public int CodTipoDocumento { get; set; }
        public DateTime? DataDocumento { get; set; }
        [StringLength(100)]
        public string NumDocumento { get; set; }
        public double ValorDocumento { get; set; }
        [StringLength(300)]
        public string UrlDocumento { get; set; }
        [StringLength(100)]
        public string NomeFornecedor { get; set; }
        [StringLength(30)]
        public string CnpjCpfFornecedor { get; set; }
        public double ValorLiquido { get; set; }
        public double ValorGlosa { get; set; }
        [StringLength(100)]
        public string NumRessarcimento { get; set; }
        public int CodLote { get; set; }
        public int Parcela { get; set; }

        public int DeputadoId { get; set; } // Foreign key property

        [ForeignKey("DeputadoId")]
        public virtual Deputado Deputado { get; set; } 
    }
}