using Entities.ValueObject;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestEntities.ValueObject;

[TestClass]
public class TestLegislatura
{
    [TestMethod]
    public void CriarLegislatura_ValidAno_ReturnsCorrectLegislatura()
    {
        // Arrange
        int ano = 1989; 
        int expectedLegislaturaNumero = 48;

        // Act
        Legislatura result = Legislatura.CriarLegislatura(ano);

        // Assert
        Assert.AreEqual(expectedLegislaturaNumero, result.Numero);
    }

    [TestMethod]
    public void CriarLegislatura_AnoAtLegislaturaBoundary_ReturnsNextLegislatura()
    {
        int ano = 1991;
        int expectedLegislaturaNumero = 49; 

        Legislatura result = Legislatura.CriarLegislatura(ano);

        Assert.AreEqual(expectedLegislaturaNumero, result.Numero);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CriarLegislatura_AnoBelowMinimum_ThrowsArgumentException()
    {
        // Arrange
        int ano = 1986;

        // Act
        Legislatura.CriarLegislatura(ano);
    }

    [TestMethod]
    public void CriarLegislatura_ValidAnoAboveBase_ReturnsCorrectLegislatura()
    {
        int ano = 2023; 
        int expectedNumero = 48 + ((2023 - 1987) / 4);

        Legislatura result = Legislatura.CriarLegislatura(ano);

        Assert.AreEqual(expectedNumero, result.Numero);
    }
}