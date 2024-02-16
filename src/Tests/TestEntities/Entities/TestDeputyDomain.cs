using Entities.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestEntities.Entities;

[TestClass]
public class TestDeputyDomain
{
    [TestMethod]
    public void CreateDeputy_ValidParameters_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        var id = "1";
        var firstName = "John";
        var lastName = "Doe";
        var fullName = "John Doe";
        var dateOfBirth = new DateTime(1980, 1, 1);
        var stateBirth = "SP";
        var cpf = "905.277.626-10";
        var gender = "Male";
        var partido = "XYZ";
        var ufRepresentacao = "SP";
        var nomeEleitoral = "JohnD";
        var emailDeputado = "john.d@example.com";
        var escolaridade = "Ensino Superior";
        var photoUrl = "https://example.com/photo.jpg";

        // Act
        var deputy = DeputyDomain.CreateDeputy(id, firstName, lastName, fullName, dateOfBirth, stateBirth, cpf, gender, 
            partido, ufRepresentacao, nomeEleitoral, emailDeputado, escolaridade, photoUrl);

        // Assert
        Assert.IsNotNull(deputy);
        Assert.AreEqual(id, deputy.Id);
        Assert.AreEqual(partido, deputy.Partido);
    }
}