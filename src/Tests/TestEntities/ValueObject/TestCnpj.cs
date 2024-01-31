using Entities.ValueObject;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestEntities.ValueObject;

[TestClass]
public class TestCnpj
{
    [DataTestMethod]
    [DataRow("00.000.000/0001-91")] // Valid CNPJ with mask
    [DataRow("00000000000191")] // Valid CNPJ without mask
    public void Constructor_ValidCnpj_ShouldNotThrow(string validCnpj)
    {
        // Act & Assert
        Cnpj cnpj = null;
        try
        {
            cnpj = new Cnpj(validCnpj);
        }
        catch (Exception ex)
        {
            Assert.Fail($"Expected no exception, but got: {ex.Message}");
        }
        Assert.IsNotNull(cnpj);
    }

    [DataTestMethod]
    [DataRow("")]
    [DataRow(null)]
    [DataRow("11111111111111")] // All identical digits
    [DataRow("12345678901234")] // Invalid check digits
    [DataRow("00.000.000/0001-00")] // Incorrect format
    public void Constructor_InvalidCnpj_ShouldThrowArgumentException(string invalidCnpj)
    {
        // Act & Assert
        Assert.ThrowsException<ArgumentException>(() => new Cnpj(invalidCnpj));
    }

    [TestMethod]
    public void ToString_ValidCnpj_ShouldReturnOriginalString()
    {
        // Arrange
        var validCnpj = "00000000000191";
        var cnpj = new Cnpj(validCnpj);

        // Act
        var result = cnpj.ToString();

        // Assert
        Assert.AreEqual(validCnpj, result);
    }
}