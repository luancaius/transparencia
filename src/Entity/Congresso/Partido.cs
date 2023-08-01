using System.ComponentModel.DataAnnotations;

namespace Entity.Congresso;

public class Partido
{
    public int Id { get; set; }
        
    [Required]
    [MaxLength(100)]
    public string Nome { get; set; }
        
    [MaxLength(10)]
    public string Sigla { get; set; }
}