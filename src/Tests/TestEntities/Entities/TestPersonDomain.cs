using Entities.Entities;
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
        Assert.AreEqual(name, person.FullName);
    }

    [TestMethod]
    public void CreatePerson_WithValidData_ReturnsPersonDomainInstance()
    {
        // Arrange
        string firstName = "Jane";
        string lastName = "Doe";
        string fullName = "Jane Doe";
        DateTime dateOfBirth = new DateTime(1990, 1, 1);
        string email = "jane.doe@example.com";
        string estadoNascimento = "SP"; 
        string municipioNascimento = "São Paulo";
        string cpf = "706.362.134-39"; 
        string gender = "Female";
        string escolaridade = "Ensino Superior";

        // Act
        var person = PersonDomain.CreatePerson(fullName, dateOfBirth, email, estadoNascimento, municipioNascimento, cpf,
            gender, escolaridade);

        // Assert
        Assert.AreEqual(fullName, person.FullName);
        Assert.AreEqual(dateOfBirth, person.DateOfBirth);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CreatePerson_WithInvalidEmail_ThrowsArgumentException()
    {
        // Arrange
        string firstName = "Invalid";
        string lastName = "User";
        string fullName = "Invalid User";
        DateTime dateOfBirth = new DateTime(2000, 1, 1);
        string email = "invalidEmail"; 
        string estadoNascimento = "SP";
        string municipioNascimento = "São Paulo";
        string cpf = "706.362.134-39";
        string gender = "Male";
        string escolaridade = "Ensino Superior";

        // Act
        PersonDomain.CreatePerson(fullName, dateOfBirth, email, estadoNascimento, municipioNascimento, cpf, gender, 
            escolaridade);
    }
}