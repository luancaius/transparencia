using Entities.ValueObject;

namespace Entities.DomainEntities;

public class Person : IEntity
{
    private Person(Guid id, string firstName, string lastName, string fullName, DateTime dateOfBirth, String email, 
        string stateBirth, Cpf cpf, Gender gender)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        FullName = fullName;
        DateOfBirth = dateOfBirth;
        StateBirth = stateBirth;
        Email = email;
        CPF = cpf;
        Gender = gender;
    }

    public static Person CreatePerson(Guid id, string firstName, string lastName, string fullName,
        DateTime dateOfBirth, string email, string stateBirth, Cpf cpf, Gender gender)
    {
        return new Person(id, firstName, lastName, fullName, dateOfBirth, stateBirth, email, cpf, gender);
    }
    
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    
    public string FullName { get; private set; }
    public DateTime DateOfBirth { get; private set; }
    public string StateBirth { get; private set; }
    public String Email { get; private set; }
    public Cpf CPF { get; private set; }
    public Gender Gender { get; private set; }
    public Guid Id { get; set; }
}
