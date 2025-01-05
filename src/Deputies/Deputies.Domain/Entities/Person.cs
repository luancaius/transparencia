using Deputies.Domain.AbstractEntities;
using Deputies.Domain.ValueObjects;

namespace Deputies.Domain.Entities;

public class Person : Participant
{
    private Person(Cpf cpf, PersonName personName)
    {
        Cpf = cpf;
        PersonName = personName;
    }

    public Cpf Cpf { get; }
    public PersonName PersonName { get; }

    public static Person Create(Cpf cpf, PersonName personName)
    {
        if (cpf == null) 
            throw new ArgumentNullException(nameof(cpf), "CPF cannot be null.");
        if (personName == null) 
            throw new ArgumentNullException(nameof(personName), "Name cannot be null.");

        return new Person(cpf, personName);
    }

    public override string DisplayName => PersonName.ToString();
    
    public override bool Equals(object obj)
    {
        if (obj is Person other)
        {
            return Cpf.Equals(other.Cpf);
        }
        return false;
    }

    public override int GetHashCode() => Cpf.GetHashCode();
}