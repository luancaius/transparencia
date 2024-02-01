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
        Assert.AreEqual("", person.LastName); 
        Assert.AreEqual(name, person.FirstName);
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
        string cpf = "706.362.134-39"; 
        string gender = "Female";

        // Act
        var person = PersonDomain.CreatePerson(firstName, lastName, fullName, dateOfBirth, email, estadoNascimento, cpf,
            gender);

        // Assert
        Assert.AreEqual(firstName, person.FirstName);
        Assert.AreEqual(lastName, person.LastName);
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
        string cpf = "706.362.134-39";
        string gender = "Male";

        // Act
        PersonDomain.CreatePerson(firstName, lastName, fullName, dateOfBirth, email, estadoNascimento, cpf, gender);
    }
}