namespace Entity.Congresso;

public class Legislatura
{
    public int Id { get; set; }
        
    public int Numero { get; set; }
        
    public DateTime Inicio { get; set; }
        
    public DateTime? Fim { get; set; }
}