using System.ComponentModel.DataAnnotations.Schema;

namespace RelationalDatabase.DTO.Deputado;

[Table("DeputyWorkPresences", Schema = "congresso")]
public class DeputyWorkPresence : BaseEntity
{
    public int Ano { get; set; }
    public int Mes { get; set; }
    public int Legislatura { get; set; }
    public int CarteiraParlamentar { get; set; }
    public string NomeParlamentar { get; set; }
    public string SiglaPartido { get; set; }
    public string SiglaUF { get; set; }

    public int DeputadoId { get; set; } // Foreign key property

    [ForeignKey("DeputadoId")]
    public virtual Deputado Deputado { get; set; } 
    
    public virtual ICollection<DiaSessao> DiasDeSessoes { get; set; }
}