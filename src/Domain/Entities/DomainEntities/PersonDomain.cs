using Entities.ValueObject;

namespace Entities.DomainEntities;

public class PersonDomain : IEntity
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string FullName { get; private set; }
    public DateTime DateOfBirth { get; private set; }
    public Estado EstadoNascimento { get; private set; }
    public Email Email { get; private set; }
    public Cpf CPF { get; private set; }
    public Gender Gender { get; private set; }
    public string Id { get; set; }

    private PersonDomain(string id, string firstName, string lastName, string fullName, DateTime dateOfBirth, String email, 
        string stateBirth, string cpf, string gender)
    {
        try
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            FullName = fullName;
            DateOfBirth = dateOfBirth;
            EstadoNascimento = Extensions.ConvertStringToEstado(stateBirth);
            Email = new Email(email);
            CPF = new Cpf(cpf); 
            Gender = GenderExtensions.FromString(gender); 
        }
        catch (ArgumentException ex)
        {
            throw new ArgumentException($"Erro ao processar dados de PersonDomain: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro inesperado ao criar PersonDomain: {ex.Message}", ex);
        }
    }

    public static PersonDomain CreatePerson(string id, string firstName, string lastName, string fullName,
        DateTime dateOfBirth, string email, string estadoNascimento, string cpf, string gender)
    {
        return new PersonDomain(id, firstName, lastName, fullName, dateOfBirth, estadoNascimento, email, cpf, gender);
    }

}
