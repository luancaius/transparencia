using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Congresso;

public class Despesa
{
    public int Id { get; set; }
        
    [ForeignKey("Deputado")]
    public int DeputadoId { get; set; }
        
    public Deputado Deputado { get; set; }
        
    public decimal Valor { get; set; }
        
    [MaxLength(200)]
    public string Descricao { get; set; }
        
    public DateTime Data { get; set; }
}