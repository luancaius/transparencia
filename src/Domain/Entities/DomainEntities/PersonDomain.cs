using Entities.ValueObject;

namespace Entities.DomainEntities;

public class PersonDomain : IEntity
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string FullName { get; private set; }
    public DateTime DateOfBirth { get; private set; }
    public string StateBirth { get; private set; }
    public Email Email { get; private set; }
    public Cpf CPF { get; private set; }
    public Gender Gender { get; private set; }
    public string Id { get; set; }

    private PersonDomain(string id, string firstName, string lastName, string fullName, DateTime dateOfBirth, String email, 
        string stateBirth, string cpf, string gender)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        FullName = fullName;
        DateOfBirth = dateOfBirth;
        StateBirth = stateBirth;
        Email = new Email(email);
        CPF = new Cpf(cpf);
        Gender = GenderExtensions.FromString(gender);
    }

    public static PersonDomain CreatePerson(string id, string firstName, string lastName, string fullName,
        DateTime dateOfBirth, string email, string stateBirth, string cpf, string gender)
    {
        return new PersonDomain(id, firstName, lastName, fullName, dateOfBirth, stateBirth, email, cpf, gender);
    }

}
