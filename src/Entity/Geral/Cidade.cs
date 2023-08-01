using System.ComponentModel.DataAnnotations;

namespace Entity.Geral;

public class Cidade
{
    public int Id { get; set; }
        
    [Required]
    [MaxLength(100)]
    public string Nome { get; set; }
}