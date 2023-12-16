using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RelationalDatabase.DTO.Deputado;

[Table("DeputyWorkPresences", Schema = "congresso")]
public class DeputyWorkPresence : BaseEntity
{
    public int Ano { get; set; }
    public int Mes { get; set; }
    public int Legislatura { get; set; }
    public int CarteiraParlamentar { get; set; }
    [StringLength(100)]
    public string NomeParlamentar { get; set; }
    [StringLength(20)]
    public string SiglaPartido { get; set; }
    [StringLength(2)]
    public string SiglaUF { get; set; }

    public int DeputadoId { get; set; } // Foreign key property

    [ForeignKey("DeputadoId")]
    public virtual Deputado Deputado { get; set; } 
    
    public virtual ICollection<DiaSessao> DiasDeSessoes { get; set; }
}