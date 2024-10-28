using Entities.Entities;
using Entities.ValueObject;
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
        var fullName = "John Doe";
        var dateOfBirth = new DateTime(1980, 1, 1);
        var stateBirth = "SP";
        var municipioNascimento = "São Paulo";
        var cpf = "905.277.626-10";
        var gender = "Male";
        var partido = "XYZ";
        var ufRepresentacao = "SP";
        var nomeEleitoral = "JohnD";
        var emailDeputado = "john.d@example.com";
        var escolaridade = "Ensino Superior";
        var photoUrl = "https://example.com/photo.jpg";
        var legislatura = 56;
        var gabinete = Gabinete.CreateGabinete("Gabinete 1", "Prédio 1", "Sala 1", "Andar 1", "123456789", "");

        // Creating PersonDomain
        var person = PersonDomain.CreatePerson(
            "John", "Doe", null, dateOfBirth, emailDeputado, stateBirth, municipioNascimento, cpf, gender, escolaridade
        );

        // Act
        var deputy = DeputyDomain.CreateDeputy(id, person, partido, ufRepresentacao, nomeEleitoral, emailDeputado,
            photoUrl, legislatura, gabinete);

        // Assert   
        Assert.IsNotNull(deputy);
        Assert.AreEqual(id, deputy.Id);
        Assert.AreEqual(partido, deputy.Partido);
        Assert.AreEqual(nomeEleitoral, deputy.NomeEleitoral);
        Assert.AreEqual(emailDeputado, deputy.EmailDeputado.Value);
        Assert.AreEqual(photoUrl, deputy.Photo.Url);
        Assert.AreEqual(legislatura, deputy.Legislatura.Numero);
        Assert.AreEqual(gabinete, deputy.Gabinete);
    }
}