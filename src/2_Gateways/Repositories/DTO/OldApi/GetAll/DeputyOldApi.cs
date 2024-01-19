namespace Repositories.DTO.OldApi.GetAll;

public class DeputyOldApi { 

    public int IdeCadastro { get; set; } 
    public string CodOrcamento { get; set; } 
    public string Condicao { get; set; } 
    public int Matricula { get; set; } 
    public int IdParlamentar { get; set; } 
    public string Nome { get; set; } 
    public string NomeParlamentar { get; set; } 
    public string UrlFoto { get; set; } 
    public string Sexo { get; set; } 
    public string Uf { get; set; } 
    public string Partido { get; set; } 
    public string Gabinete { get; set; } 
    public string Anexo { get; set; } 
    public string Fone { get; set; } 
    public string Email { get; set; } 
    public Comissoes Comissoes { get; set; }

    public override string ToString()
    {
        return $"Nome: {Nome} idCadastro:{IdeCadastro} idParlamentar:{IdParlamentar}";
    }
}
