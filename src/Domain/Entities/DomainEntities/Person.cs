using Entities.ValueObject;

namespace Entities.DomainEntities;

public class Person : IEntity
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string FullName { get; private set; }
    public DateTime DateOfBirth { get; private set; }
    public string StateBirth { get; private set; }
    public Email Email { get; private set; }
    public Cpf CPF { get; private set; }
    public Gender Gender { get; private set; }
    public Guid Id { get; set; }
    
    private Person(Guid id, string firstName, string lastName, string fullName, DateTime dateOfBirth, String email, 
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

    public static Person CreatePerson(Guid id, string firstName, string lastName, string fullName,
        DateTime dateOfBirth, string email, string stateBirth, string cpf, string gender)
    {
        return new Person(id, firstName, lastName, fullName, dateOfBirth, stateBirth, email, cpf, gender);
    }
}
