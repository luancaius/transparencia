using System;
using Xunit;
using Deputies.Domain.ValueObjects;

public class CnpjTests
{
    [Fact]
    public void Should_Create_Cnpj_With_Unmasked_Value()
    {
        // Arrange
        var unmaskedCnpj = "12345678000195";

        // Act
        var cnpj = new Cnpj(unmaskedCnpj);

        // Assert
        Assert.Equal("12.345.678/0001-95", cnpj.GetMasked());
        Assert.Equal("12345678000195", cnpj.GetUnmasked());
    }

    [Fact]
    public void Should_Create_Cnpj_With_Masked_Value()
    {
        // Arrange
        var maskedCnpj = "12.345.678/0001-95";

        // Act
        var cnpj = new Cnpj(maskedCnpj);

        // Assert
        Assert.Equal("12.345.678/0001-95", cnpj.GetMasked());
        Assert.Equal("12345678000195", cnpj.GetUnmasked());
    }

    [Fact]
    public void Should_Throw_Exception_For_Invalid_Cnpj()
    {
        // Arrange
        var invalidCnpj = "12345678000100"; // Invalid CNPJ (fails checksum)

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Cnpj(invalidCnpj));
    }

    [Fact]
    public void Should_Return_Masked_Cnpj_In_ToString()
    {
        // Arrange
        var cnpj = new Cnpj("12345678000195");

        // Act
        var result = cnpj.ToString();

        // Assert
        Assert.Equal("12.345.678/0001-95", result);
    }

    [Fact]
    public void Should_Be_Equal_For_Same_Cnpj_Value_With_Different_Masks()
    {
        // Arrange
        var cnpj1 = new Cnpj("12.345.678/0001-95"); // Masked format
        var cnpj2 = new Cnpj("12345678000195");      // Unmasked format

        // Act & Assert
        Assert.Equal(cnpj1, cnpj2); // Should be equal because they represent the same CNPJ
    }

    [Fact]
    public void Should_Have_Same_HashCode_For_Same_Cnpj_Value()
    {
        // Arrange
        var cnpj1 = new Cnpj("12.345.678/0001-95"); // Masked format
        var cnpj2 = new Cnpj("12345678000195");      // Unmasked format

        // Act & Assert
        Assert.Equal(cnpj1.GetHashCode(), cnpj2.GetHashCode()); // Same hash code for equivalent CNPJs
    }

    [Fact]
    public void Should_Not_Be_Equal_For_Different_Cnpj_Values()
    {
        // Arrange
        var cnpj1 = new Cnpj("12345678000195");
        var cnpj2 = new Cnpj("98765432000100");

        // Act & Assert
        Assert.NotEqual(cnpj1, cnpj2); // Different CNPJs should not be equal
    }

    [Fact]
    public void Should_Throw_Exception_For_Empty_Cnpj()
    {
        // Arrange
        var emptyCnpj = "";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Cnpj(emptyCnpj));
    }
}
