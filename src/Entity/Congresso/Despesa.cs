using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Congresso;

public class Despesa : BaseEntity
{        
    [ForeignKey("Deputado")]
    public Deputado Deputado { get; set; }
        
    public decimal Valor { get; set; }
        
    [MaxLength(200)]
    public string Descricao { get; set; }
        
    public DateTime Data { get; set; }
}