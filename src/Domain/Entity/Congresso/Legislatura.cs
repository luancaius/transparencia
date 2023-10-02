namespace Entity.Congresso;

public class Legislatura : BaseEntity
{        
    public int Numero { get; set; }
        
    public DateTime Inicio { get; set; }
        
    public DateTime? Fim { get; set; }
}