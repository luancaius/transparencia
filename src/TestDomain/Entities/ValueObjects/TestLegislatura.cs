using Entities.ValueObject;
using NUnit.Framework;

namespace TestDomain.Entities.ValueObjects;

[TestFixture]
public class LegislaturaTests
{
    [Test]
    public void CriarLegislaturaPorAno_YearWithin48thLegislature_Returns48()
    {
        // Arrange
        int inputYear = 1988;
        int expectedLegislaturaNumero = 48;

        // Act
        var result = Legislatura.CriarLegislaturaPorAno(inputYear);

        // Assert
        Assert.AreEqual(expectedLegislaturaNumero, result.Numero);
    }

    [Test]
    public void CriarLegislaturaPorAno_YearWithin57thLegislature_Returns57()
    {
        // Arrange
        int inputYear = 2025;
        int expectedLegislaturaNumero = 57;

        // Act
        var result = Legislatura.CriarLegislaturaPorAno(inputYear);

        // Assert
        Assert.AreEqual(expectedLegislaturaNumero, result.Numero);
    }

    [Test]
    public void CriarLegislaturaPorAno_YearLessThan1987_ThrowsArgumentException()
    {
        // Arrange
        int inputYear = 1986;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => Legislatura.CriarLegislaturaPorAno(inputYear));
    }
}