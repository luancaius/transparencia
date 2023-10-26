using Repositories.DTO.NewApi.GetAll;
using Repositories.DTO.NewApi.GetById;
using Repositories.DTO.OldApi.GetAll;
using Repositories.DTO.OldApi.GetById;

namespace Services.DTO;

public class DeputyDetailDto
{
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

    public DeputyDetailDto(DeputyDetailOldApi deputyDetailOldApi, DeputyDetailNewApi deputyDetailNewApi)
    {
        // From DeputyDetailNewApi
        Id = deputyDetailNewApi.Id;
        Uri = deputyDetailNewApi.Uri;
        Nome = deputyDetailNewApi.Nome;
        SiglaPartido = deputyDetailNewApi.SiglaPartido;
        UriPartido = deputyDetailNewApi.UriPartido;
        SiglaUf = deputyDetailNewApi.SiglaUf;
        IdLegislatura = deputyDetailNewApi.IdLegislatura;
        UrlFoto = deputyDetailNewApi.UrlFoto;
        Email = deputyDetailNewApi.Email;

        // From DeputyDetailOldApi
        IdeCadastro = deputyDetailOldApi.IdeCadastro;
        CodOrcamento = ""; // This value seems to be missing from both API models
        Condicao = ""; // This value seems to be missing from both API models
        Matricula = 0; // This value seems to be missing from both API models
        IdParlamentar = 0; // This value seems to be missing from both API models
        NomeParlamentar = deputyDetailOldApi.NomeParlamentar;
        Sexo = deputyDetailOldApi.Sexo;
        Uf = deputyDetailOldApi.Uf;
        Partido = deputyDetailOldApi.Partido;
        Gabinete = deputyDetailOldApi.Gabinete;
        Anexo = deputyDetailOldApi.Anexo;
        Fone = deputyDetailOldApi.Fone;
        Comissoes = new Comissoes(deputyDetailOldApi.Comissoes);
    }
}