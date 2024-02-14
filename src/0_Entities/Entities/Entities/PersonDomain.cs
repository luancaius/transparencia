using Entities.ValueObject;

namespace Entities.Entities;

public class PersonDomain
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string FullName { get; private set; }
    public DateTime? DateOfBirth { get; private set; }
    public Estado EstadoNascimento { get; private set; }
    public Email Email { get; private set; }
    public Cpf CPF { get; private set; }
    public Gender Gender { get; private set; }
    public Escolaridade  Escolaridade { get; private set; }

    private PersonDomain(string firstName, string lastName, string fullName, DateTime dateOfBirth, String email, 
        string stateBirth, string cpf, string gender, string escolaridade)
    {
        try
        {
            FirstName = firstName;
            LastName = lastName;
            FullName = fullName;
            DateOfBirth = dateOfBirth;
            EstadoNascimento = stateBirth.ConvertStringToEstado();
            Email = new Email(email);
            CPF = new Cpf(cpf); 
            Gender = Extensions.GenderFromString(gender); 
            Escolaridade = Extensions.EscolaridadeFromString(escolaridade);
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

    public static PersonDomain CreateSimplePerson(string name, string cpf)
    {
        return new PersonDomain(name, "", name, DateTime.MinValue, "", "", cpf, "", "");
    }
    
    public static PersonDomain CreatePerson(string firstName, string lastName, string fullName,
        DateTime dateOfBirth, string email, string estadoNascimento, string cpf, string gender, string escolaridade)
    {
        return new PersonDomain(firstName, lastName, fullName, dateOfBirth, email,estadoNascimento, cpf, gender, escolaridade);
    }
}
