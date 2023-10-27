using Repositories.DTO.NewApi.GetAll;
using Repositories.DTO.OldApi.GetAll;

namespace Services.DTO;

public class DeputyDto
{
    // Properties from DeputyNewApi
    public string Id { get; set; }
    public string Uri { get; set; }
    public string Nome { get; set; } // Renamed to avoid conflict
    public string SiglaPartido { get; set; }
    public string UriPartido { get; set; }
    public string SiglaUf { get; set; }
    public int IdLegislatura { get; set; }
    public string UrlFoto { get; set; } // Renamed to avoid conflict
    public string Email { get; set; } // Renamed to avoid conflict

    // Properties from DeputyOldApi
    public int IdeCadastro { get; set; } 
    public string CodOrcamento { get; set; } 
    public string Condicao { get; set; } 
    public int Matricula { get; set; } 
    public int IdParlamentar { get; set; } 
    public string NomeParlamentar { get; set; } 
    public string Sexo { get; set; } 
    public string Uf { get; set; } 
    public string Partido { get; set; } 
    public string Gabinete { get; set; } 
    public string Anexo { get; set; } 
    public string Fone { get; set; } 
    public Comissoes Comissoes { get; set; }

    public DeputyDto(DeputyNewApi newApi, DeputyOldApi oldApi)
    {
        // Properties from DeputyNewApi
        Id = newApi.Id;
        Uri = newApi.Uri;
        Nome = newApi.Nome;
        SiglaPartido = newApi.SiglaPartido;
        UriPartido = newApi.UriPartido;
        SiglaUf = newApi.SiglaUf;
        IdLegislatura = newApi.IdLegislatura;
        UrlFoto = newApi.UrlFoto;
        Email = newApi.Email;

        // Properties from DeputyOldApi
        IdeCadastro = oldApi.IdeCadastro;
        CodOrcamento = oldApi.CodOrcamento;
        Condicao = oldApi.Condicao;
        Matricula = oldApi.Matricula;
        IdParlamentar = oldApi.IdParlamentar;
        NomeParlamentar = oldApi.NomeParlamentar;
        Sexo = oldApi.Sexo;
        Uf = oldApi.Uf;
        Partido = oldApi.Partido;
        Gabinete = oldApi.Gabinete;
        Anexo = oldApi.Anexo;
        Fone = oldApi.Fone;
        Comissoes = new Comissoes(oldApi.Comissoes);
    }
    
    public DeputyDto(DeputyNewApi newApi){
        // Properties from DeputyNewApi
        Id = newApi.Id;
        Uri = newApi.Uri;
        Nome = newApi.Nome;
        SiglaPartido = newApi.SiglaPartido;
        UriPartido = newApi.UriPartido;
        SiglaUf = newApi.SiglaUf;
        IdLegislatura = newApi.IdLegislatura;
        UrlFoto = newApi.UrlFoto;
        Email = newApi.Email;
    }
    
}