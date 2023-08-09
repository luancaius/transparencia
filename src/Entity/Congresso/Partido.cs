using System.ComponentModel.DataAnnotations;

namespace Entity.Congresso;

public class Partido : BaseEntity
{        
    [Required]
    [MaxLength(100)]
    public string Nome { get; set; }
        
    [MaxLength(10)]
    public string Sigla { get; set; }
    
    public List<Deputado> Deputados { get; set; }
}