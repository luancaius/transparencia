using System.ComponentModel.DataAnnotations;

namespace Entity.Geral;

public class Cidade : BaseEntity
{        
    [Required]
    [MaxLength(100)]
    public string Nome { get; set; }
}