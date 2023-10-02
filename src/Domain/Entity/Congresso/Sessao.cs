using System.ComponentModel.DataAnnotations;

namespace Entity.Congresso;

public class Sessao : BaseEntity
{        
    public DateTime Data { get; set; }
        
    [MaxLength(200)]
    public string Descricao { get; set; }
}