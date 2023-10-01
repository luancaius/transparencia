using Entities.ValueObject;

namespace Entities.DomainEntities;

public class Person : IEntity
{
    public Person(Guid id, string firstName, string lastName, DateTime dateOfBirth, String email, Phone phone, Address address, Cpf cpf, Gender gender)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        Email = email;
        Phone = phone;
        Address = address;
        CPF = cpf;
        Gender = gender;
    }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public DateTime DateOfBirth { get; private set; }
    public String Email { get; private set; }
    public Phone Phone { get; private set; }
    public Address Address { get; private set; }
    public Cpf CPF { get; private set; }
    public Gender Gender { get; private set; }
    public Guid Id { get; set; }
}

public interface IEntity
{
    public Guid Id { get; protected set; }
}
