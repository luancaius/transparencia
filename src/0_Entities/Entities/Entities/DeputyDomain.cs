using Entities.ValueObject;

namespace Entities.DomainEntities;

public class DeputyDomain
{
    public string Id { get; private set; }
    public PersonDomain Person { get; private set; }
    public string Partido { get; private set; }
    public Estado EstadoRepresentacao { get; private set; }
    public string NomeEleitoral { get; private set; }
    public Email EmailDeputado { get; private set; }
    
    private DeputyDomain(string id, string firstName, string lastName, string fullName, DateTime dateOfBirth, String email,
        string stateBirth, string cpf, string gender, string partido, string estadoRepresentacao, string nomeEleitoral, string emailDeputado)
    {
        Id = id;
        Person = PersonDomain.CreatePerson(firstName, lastName, fullName, dateOfBirth, stateBirth, email, cpf, gender);
        Partido = partido;
        EstadoRepresentacao = estadoRepresentacao.ConvertStringToEstado();
        NomeEleitoral = nomeEleitoral;
        EmailDeputado = new Email(emailDeputado);
    }

    public static DeputyDomain CreateDeputy(string id, string firstName, string lastName, string fullName,
        DateTime dateOfBirth, string stateBirth, string cpf, string gender,
        string partido, string ufRepresentacao, string nomeEleitoral, string emailDeputado)
    {
        return new DeputyDomain(id, firstName, lastName, fullName, dateOfBirth, "", stateBirth, cpf, 
            gender, partido, ufRepresentacao, nomeEleitoral, emailDeputado);
    }
}