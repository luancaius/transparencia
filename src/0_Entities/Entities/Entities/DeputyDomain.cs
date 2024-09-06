using Entities.ValueObject;

namespace Entities.Entities;

public class DeputyDomain
{
    public string Id { get; private set; }
    public PersonDomain Person { get; private set; }
    public string Partido { get; private set; }
    public Estado EstadoRepresentacao { get; private set; }
    public string NomeEleitoral { get; private set; }
    public Email EmailDeputado { get; private set; }
    public Image Photo { get; private set; }
    public Legislatura Legislatura { get; private set; }
    public Gabinete Gabinete { get; private set; }

    private DeputyDomain(string id, PersonDomain personDomain, string partido, Estado estadoRepresentacao,
        string nomeEleitoral, Email emailDeputado, Image photoDeputy, Legislatura legislatura, Gabinete gabinete)
    {
        Id = id;
        Person = personDomain;
        Partido = partido;
        EstadoRepresentacao = estadoRepresentacao;
        NomeEleitoral = nomeEleitoral;
        EmailDeputado = emailDeputado;
        Photo = photoDeputy;
        Legislatura = legislatura;
        Gabinete = gabinete;
    }

    public static DeputyDomain CreateDeputy(string id, PersonDomain person, string partido, 
        string ufRepresentacao, string nomeEleitoral, string emailDeputadoString, string deputyPhotoUrl, 
        int legislatura, Gabinete gabinete)
    {
        try
        {
            var photo = new Image(deputyPhotoUrl, "Foto do deputado");
            var ufRepresentacaoEnum = ufRepresentacao.ConvertStringToEstado();
            var emailDeputado = new Email(emailDeputadoString);
            var legislaturaVO = Legislatura.CriarLegislatura(legislatura);
            return new DeputyDomain(id, person, partido, ufRepresentacaoEnum, nomeEleitoral, emailDeputado, photo, legislaturaVO, gabinete);
        }
        catch (ArgumentException ex)
        {
            throw new ArgumentException($"Erro ao processar dados de DeputyDomain: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro inesperado ao criar DeputyDomain: {ex.Message}", ex);
        }
    }
}