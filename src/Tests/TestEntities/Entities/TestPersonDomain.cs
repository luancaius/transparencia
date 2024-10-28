using Entities.Entities;
using Entities.ValueObject;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestEntities.Entities;

[TestClass]
public class TestPersonDomain
{
    [TestMethod]
    public void CreateSimplePerson_WithValidData_ReturnsPersonDomainInstance()
    {
        // Arrange
        string name = "John Doe";
        string cpf = "706.362.134-39";

        // Act
        var person = PersonDomain.CreateSimplePerson(name, cpf);

        // Assert
        Assert.AreEqual(name, person.Name.FirstName);
        Assert.AreEqual(cpf, person.CPF.ToString());
    }

    [TestMethod]
    public void CreatePerson_WithValidData_ReturnsPersonDomainInstance()
    {
        // Arrange
        string firstName = "Jane";
        string lastName = "Doe";
        string nickname = null;
        DateTime dateOfBirth = new DateTime(1990, 1, 1);
        string email = "jane.doe@example.com";
        string estadoNascimento = "SP";
        string municipioNascimento = "São Paulo";
        string cpf = "706.362.134-39";
        string gender = "Female";
        string escolaridade = "Superior Completo";

        // Act
        var person = PersonDomain.CreatePerson(firstName, lastName, nickname, dateOfBirth, email, estadoNascimento,
            municipioNascimento, cpf, gender, escolaridade);

        // Assert
        Assert.AreEqual($"{firstName} {lastName}", person.Name.FirstName+" "+person.Name.LastName);
        Assert.AreEqual(dateOfBirth, person.DateOfBirth);
        Assert.AreEqual(email, person.Email.Value);
        Assert.AreEqual(estadoNascimento,
            person.EstadoNascimento.ToString()); // Assuming ConvertStringToEstado is implemented
        Assert.AreEqual(cpf, person.CPF.ToString());
        Assert.AreEqual(gender, person.Gender.ToString());
        Assert.AreEqual(Escolaridade.SuperiorCompleto, person.Escolaridade);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CreatePerson_WithInvalidEmail_ThrowsArgumentException()
    {
        // Arrange
        string firstName = "Invalid";
        string lastName = "User";
        string nickname = null;
        DateTime dateOfBirth = new DateTime(2000, 1, 1);
        string email = "invalidEmail";
        string estadoNascimento = "SP";
        string municipioNascimento = "São Paulo";
        string cpf = "706.362.134-39";
        string gender = "Male";
        string escolaridade = "Ensino Superior";

        // Act
        PersonDomain.CreatePerson(firstName, lastName, nickname, dateOfBirth, email, estadoNascimento,
            municipioNascimento, cpf, gender, escolaridade);
    }
}