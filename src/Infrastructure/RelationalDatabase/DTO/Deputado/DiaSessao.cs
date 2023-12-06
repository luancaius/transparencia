using System.ComponentModel.DataAnnotations.Schema;

namespace RelationalDatabase.DTO.Deputado;

[Table("DiaSessao", Schema = "congresso")]
public class DiaSessao : BaseEntity
{
    public DateTime Data { get; set; }
    public string FrequenciaNoDia { get; set; }
    public string Justificativa { get; set; }
    public int QtdeSessoes { get; set; }
    
    public int DeputadoId { get; set; } // Foreign key property

    [ForeignKey("DeputadoId")]
    public virtual Deputado Deputado { get; set; } 
}