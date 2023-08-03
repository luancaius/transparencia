using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Congresso;

public class Deputado
{
    public int Id { get; set; }
        
    [Required]
    [MaxLength(100)]
    public string Nome { get; set; }
        
    [ForeignKey("Partido")]
    public int PartidoId { get; set; }
        
    public Partido Partido { get; set; }
        
    [ForeignKey("Legislatura")]
    public int LegislaturaId { get; set; }
        
    public Legislatura Legislatura { get; set; }

    public override string ToString()
    {
        return $"Nome: {Nome}, Partido: {Partido?.Nome}, Legislatura: {Legislatura?.Numero}";
    }
}