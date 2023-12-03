using Repositories.DTO.OldApi.GetById;
using Repositories.DTO;

namespace Services.DTO;

public class DeputyDetailDto : BaseEntityDTO
{
    // Properties from DeputyDetailNewApi
    public int IdDeputy { get; set; }
    public string Uri { get; set; }
    public string Nome { get; set; }
    public string SiglaPartido { get; set; }
    public string UriPartido { get; set; }
    public string SiglaUf { get; set; }
    public int IdLegislatura { get; set; }
    public string UrlFoto { get; set; }
    public string Email { get; set; }

    // Properties from DeputyDetailOldApi
    public int IdeCadastro { get; set; }
    public string NomeCivil { get; set; }
    public string NomeParlamentarAtual { get; set; }
    public string Sexo { get; set; }
    public string UfRepresentacaoAtual { get; set; }
    public string SituacaoNaLegislaturaAtual { get; set; }
    public int IdParlamentarDeprecated { get; set; }
    public PartidoAtual PartidoAtual { get; set; }
    public Gabinete Gabinete { get; set; }
    public List<Comissao> Comissoes { get; set; }

    public DeputyDetailDto(DeputyDetailOldApi deputyDetailOldApi, DeputyDetailNewApi deputyDetailNewApi)
    {
        // From DeputyDetailNewApi
        IdDeputy = deputyDetailNewApi.IdDeputy;
        Uri = deputyDetailNewApi.Uri;
        Nome = deputyDetailNewApi.Nome;
        SiglaPartido = deputyDetailNewApi.SiglaPartido;
        UriPartido = deputyDetailNewApi.UriPartido;
        SiglaUf = deputyDetailNewApi.SiglaUf;
        IdLegislatura = deputyDetailNewApi.IdLegislatura;
        UrlFoto = deputyDetailNewApi.UrlFoto;
        Email = deputyDetailNewApi.Email;

        // From DeputyDetailOldApi
        IdeCadastro = deputyDetailOldApi.IdDeputy;
        NomeCivil = deputyDetailOldApi.NomeCivil;
        NomeParlamentarAtual = deputyDetailOldApi.NomeParlamentarAtual;
        Sexo = deputyDetailOldApi.Sexo;
        UfRepresentacaoAtual = deputyDetailOldApi.UfRepresentacaoAtual;
        SituacaoNaLegislaturaAtual = deputyDetailOldApi.SituacaoNaLegislaturaAtual;
        IdParlamentarDeprecated = deputyDetailOldApi.IdParlamentarDeprecated;
        PartidoAtual = deputyDetailOldApi.PartidoAtual;
        Gabinete = deputyDetailOldApi.Gabinete;
        Comissoes = deputyDetailOldApi.Comissoes;

        Id = $"{IdDeputy}-{IdeCadastro}";
    }
}
