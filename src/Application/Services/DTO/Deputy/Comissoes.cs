namespace Services.DTO.Deputy;

public class Comissoes {
    public object? Titular { get; set; } 
    public object? Suplente { get; set; }

    public Comissoes(Repositories.DTO.OldApi.GetAll.Comissoes comissoes)
    {
        Titular = comissoes.Titular;
        Suplente = comissoes.Suplente;
    }
    
    public Comissoes(Repositories.DTO.OldApi.GetById.Comissoes comissoes)
    {
        Titular = comissoes.Titular;
        Suplente = comissoes.Suplente;
    }
}