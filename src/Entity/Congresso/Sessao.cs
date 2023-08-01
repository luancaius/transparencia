using System.ComponentModel.DataAnnotations;

namespace Entity.Congresso;

public class Sessao
{
    public int Id { get; set; }
        
    public DateTime Data { get; set; }
        
    [MaxLength(200)]
    public string Descricao { get; set; }
}