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
        
    }
}