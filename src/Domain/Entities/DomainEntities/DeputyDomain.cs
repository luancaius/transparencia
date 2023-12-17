using Entities.ValueObject;

namespace Entities.DomainEntities;

public class DeputyDomain : IEntity
{
    private DeputyDomain(Guid id, string firstName, string lastName, string fullName, DateTime dateOfBirth, String email,
        string stateBirth, string cpf, string gender, string partido, string ufRepresentacao, string nomeEleitoral, string emailDeputado)
    {
        Person = PersonDomain.CreatePerson(id, firstName, lastName, fullName, dateOfBirth, stateBirth, email, cpf, gender);
        Partido = partido;
        UfRepresentacao = ufRepresentacao;
        NomeEleitoral = nomeEleitoral;
        EmailDeputado = new Email(emailDeputado);
    }

    public static DeputyDomain CreateDeputy(Guid id, string firstName, string lastName, string fullName,
        DateTime dateOfBirth, string email, string stateBirth, string cpf, string gender,
        string partido, string ufRepresentacao, string nomeEleitoral, string emailDeputado)
    {
        return new DeputyDomain(id, firstName, lastName, fullName, dateOfBirth, stateBirth, email, cpf, 
            gender, partido, ufRepresentacao, nomeEleitoral, emailDeputado);
    }
    
    public Guid Id { get; set; }
    public PersonDomain Person { get; private set; }
    public string Partido { get; private set; }
    public string UfRepresentacao { get; private set; }
    public string NomeEleitoral { get; private set; }
    public Email EmailDeputado { get; private set; }
}