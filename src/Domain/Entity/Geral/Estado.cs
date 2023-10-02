using System.ComponentModel.DataAnnotations;

namespace Entity.Geral;

public class Estado : BaseEntity
{        
    [Required]
    [MaxLength(100)]
    public string Nome { get; set; }
        
    [MaxLength(2)]
    public string Sigla { get; set; }
}