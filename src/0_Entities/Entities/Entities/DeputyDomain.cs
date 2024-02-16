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

    private DeputyDomain(string id, PersonDomain personDomain, string partido, Estado estadoRepresentacao,
        string nomeEleitoral, Email emailDeputado, Image photoDeputy)
    {
        Id = id;
        Person = personDomain;
        Partido = partido;
        EstadoRepresentacao = estadoRepresentacao;
        NomeEleitoral = nomeEleitoral;
        EmailDeputado = emailDeputado;
        Photo = photoDeputy;
    }

    public static DeputyDomain CreateDeputy(string id, string firstName, string lastName, string fullName,
        DateTime dateOfBirth, string stateBirth, string cpf, string gender, string partido, string ufRepresentacao, 
        string nomeEleitoral, string emailDeputadoString, string escolaridade, string deputyPhotoUrl)
    {
        try
        {
            var person = PersonDomain.CreatePerson(firstName, lastName, fullName, dateOfBirth, "",
                stateBirth, cpf, gender, escolaridade);
            var photo = new Image(deputyPhotoUrl, "Foto do deputado");
            var ufRepresentacaoEnum = ufRepresentacao.ConvertStringToEstado();
            var emailDeputado = new Email(emailDeputadoString);
            return new DeputyDomain(id, person, partido, ufRepresentacaoEnum, nomeEleitoral, emailDeputado, photo);
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