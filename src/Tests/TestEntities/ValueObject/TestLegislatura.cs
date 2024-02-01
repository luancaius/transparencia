using Entities.ValueObject;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestEntities.ValueObject;

[TestClass]
public class TestLegislatura
{
    [TestMethod]
    public void CriarLegislaturaPorAno_ValidAno_ReturnsCorrectLegislatura()
    {
        // Arrange
        int ano = 1989; 
        int expectedLegislaturaNumero = 48;

        // Act
        Legislatura result = Legislatura.CriarLegislaturaPorAno(ano);

        // Assert
        Assert.AreEqual(expectedLegislaturaNumero, result.Numero);
    }

    [TestMethod]
    public void CriarLegislaturaPorAno_AnoAtLegislaturaBoundary_ReturnsNextLegislatura()
    {
        int ano = 1991;
        int expectedLegislaturaNumero = 49; 

        Legislatura result = Legislatura.CriarLegislaturaPorAno(ano);

        Assert.AreEqual(expectedLegislaturaNumero, result.Numero);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CriarLegislaturaPorAno_AnoBelowMinimum_ThrowsArgumentException()
    {
        // Arrange
        int ano = 1986;

        // Act
        Legislatura.CriarLegislaturaPorAno(ano);
    }

    [TestMethod]
    public void CriarLegislaturaPorAno_ValidAnoAboveBase_ReturnsCorrectLegislatura()
    {
        int ano = 2023; 
        int expectedNumero = 48 + ((2023 - 1987) / 4);

        Legislatura result = Legislatura.CriarLegislaturaPorAno(ano);

        Assert.AreEqual(expectedNumero, result.Numero);
    }
}