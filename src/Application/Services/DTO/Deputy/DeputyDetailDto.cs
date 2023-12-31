using Entities.DomainEntities;
using Repositories.DTO;
using Repositories.DTO.NewApi.GetById;
using Repositories.DTO.OldApi.GetById;
using Comissao = Repositories.DTO.OldApi.GetById.Comissao;
using Gabinete = Repositories.DTO.OldApi.GetById.Gabinete;
using PartidoAtual = Repositories.DTO.OldApi.GetById.PartidoAtual;

namespace Services.DTO.Deputy;

public class DeputyDetailDto : BaseEntityDTO
{
    // Properties from DeputyDetailNewApi
    public int IdDeputy { get; set; }
    public string Uri { get; set; }
    public string NomeCivil { get; set; }
    public string Cpf { get; set; }
    public string Sexo { get; set; }
    public DateTime DataNascimento { get; set; }
    public DateTime? DataFalecimento { get; set; }
    public string UfNascimento { get; set; }
    public string MunicipioNascimento { get; set; }
    public string Escolaridade { get; set; }
    public string UrlWebsite { get; set; }
    public List<string> RedeSocial { get; set; }
    public string Nome { get; set; }
    public string SiglaPartido { get; set; }
    public string UriPartido { get; set; }
    public string SiglaUf { get; set; }
    public int IdLegislatura { get; set; }
    public string UrlFoto { get; set; }
    public string Email { get; set; }
    public DateTime? Data { get; set; }
    public string NomeEleitoral { get; set; }
    public string Situacao { get; set; }
    public string CondicaoEleitoral { get; set; }
    public GabineteNewApi GabineteInfo { get; set; }

    // Properties from DeputyDetailOldApi
    public int IdeCadastro { get; set; }
    public string NomeParlamentarAtual { get; set; }
    public string UfRepresentacaoAtual { get; set; }
    public string SituacaoNaLegislaturaAtual { get; set; }
    public int IdParlamentarDeprecated { get; set; }
    public PartidoAtual PartidoAtual { get; set; }
    public Gabinete Gabinete { get; set; }
    public List<Comissao> Comissoes { get; set; }
    public string EmailOldApi { get; set; }
    public List<PeriodoExercicio> PeriodosExercicio { get; set; }
    public List<ItemHistoricoLider> HistoricoLider { get; set; }

    public override string ToString()
    {
        return $"{IdDeputy}-{IdeCadastro}-{Nome}-{Cpf}";
    }

    public DeputyDetailDto(DeputyDetailNewApi deputyDetailNewApi, DeputyDetailOldApi? deputyDetailOldApi = null)
    {
        // From DeputyDetailNewApi
        IdDeputy = deputyDetailNewApi.IdDeputy;
        Uri = deputyDetailNewApi.Uri;
        NomeCivil = deputyDetailNewApi.NomeCivil;
        Cpf = deputyDetailNewApi.Cpf;
        Sexo = deputyDetailNewApi.Sexo;
        DataNascimento = deputyDetailNewApi.DataNascimento;
        DataFalecimento = deputyDetailNewApi.DataFalecimento;
        UfNascimento = deputyDetailNewApi.UfNascimento;
        MunicipioNascimento = deputyDetailNewApi.MunicipioNascimento;
        Escolaridade = deputyDetailNewApi.Escolaridade;
        UrlWebsite = deputyDetailNewApi.UrlWebsite;
        RedeSocial = deputyDetailNewApi.RedeSocial;
        Nome = deputyDetailNewApi.Nome;
        SiglaPartido = deputyDetailNewApi.SiglaPartido;
        UriPartido = deputyDetailNewApi.UriPartido;
        SiglaUf = deputyDetailNewApi.SiglaUf;
        IdLegislatura = deputyDetailNewApi.IdLegislatura;
        UrlFoto = deputyDetailNewApi.UrlFoto;
        Email = deputyDetailNewApi.Email;
        Data = deputyDetailNewApi.Data;
        NomeEleitoral = deputyDetailNewApi.NomeEleitoral;
        Situacao = deputyDetailNewApi.Situacao;
        CondicaoEleitoral = deputyDetailNewApi.CondicaoEleitoral;
        GabineteInfo = new GabineteNewApi(deputyDetailNewApi.GabineteInfo);

        // From DeputyDetailOldApi
        if (deputyDetailOldApi == null)
            Id = $"{IdDeputy}-0";
        else
        {
            IdeCadastro = deputyDetailOldApi.IdDeputy;
            NomeParlamentarAtual = deputyDetailOldApi.NomeParlamentarAtual;
            UfRepresentacaoAtual = deputyDetailOldApi.UfRepresentacaoAtual;
            SituacaoNaLegislaturaAtual = deputyDetailOldApi.SituacaoNaLegislaturaAtual;
            IdParlamentarDeprecated = deputyDetailOldApi.IdParlamentarDeprecated;
            PartidoAtual = deputyDetailOldApi.PartidoAtual;
            Gabinete = deputyDetailOldApi.Gabinete;
            Comissoes = deputyDetailOldApi.Comissoes;
            EmailOldApi = deputyDetailOldApi.Email;
            PeriodosExercicio = deputyDetailOldApi.PeriodosExercicio;
            HistoricoLider = deputyDetailOldApi.HistoricoLider;

            Id = $"{IdDeputy}-{IdeCadastro}";
        }
    }

    public class GabineteNewApi
    {
        public string Nome { get; set; }
        public string Predio { get; set; }
        public string Sala { get; set; }
        public string Andar { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }

        public GabineteNewApi(DeputyDetailNewApi.Gabinete gabinete)
        {
            Nome = gabinete.Nome;
            Predio = gabinete.Predio;
            Sala = gabinete.Sala;
            Andar = gabinete.Andar;
            Telefone = gabinete.Telefone;
            Email = gabinete.Email;
        }
    }

    public static DeputyDomain GetDeputyDomainFromDto(DeputyDetailDto deputyDetailDto)
    {
        try
        {
            var splitName = deputyDetailDto.NomeCivil.Split(' ');
            var firstName = splitName[0];
            var lastName = splitName[1..].Aggregate((i, j) => i + " " + j);
            var deputado = DeputyDomain.CreateDeputy
            (
                deputyDetailDto.IdDeputy.ToString(),
                firstName,
                lastName,
                deputyDetailDto.NomeCivil,
                deputyDetailDto.DataNascimento,
                deputyDetailDto.UfNascimento,
                deputyDetailDto.Cpf,
                deputyDetailDto.Sexo,
                deputyDetailDto.SiglaPartido,
                deputyDetailDto.UfRepresentacaoAtual,
                deputyDetailDto.NomeEleitoral,
                deputyDetailDto.Email
            );
            return deputado;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}