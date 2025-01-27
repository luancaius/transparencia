using Deputies.Domain.ValueObjects;

namespace Deputies.Domain.UnitTests.ValueObjects;

public class CpfTests
{
    [Fact]
    public void Should_Create_Cpf_With_Unmasked_Value()
    {
        // Arrange
        var unmaskedCpf = "12345678909";

        // Act
        var cpf = new Cpf(unmaskedCpf);

        // Assert
        Assert.Equal("123.456.789-09", cpf.GetMasked());
        Assert.Equal("12345678909", cpf.GetUnmasked());
    }

    [Fact]
    public void Should_Create_Cpf_With_Masked_Value()
    {
        // Arrange
        var maskedCpf = "123.456.789-09";

        // Act
        var cpf = new Cpf(maskedCpf);

        // Assert
        Assert.Equal("123.456.789-09", cpf.GetMasked());
        Assert.Equal("12345678909", cpf.GetUnmasked());
    }

    [Fact]
    public void Should_Throw_Exception_For_Invalid_Cpf()
    {
        // Arrange
        var invalidCpf = "12345678900"; // Invalid CPF (fails checksum)

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Cpf(invalidCpf));
    }

    [Fact]
    public void Should_Return_Masked_Cpf_In_ToString()
    {
        // Arrange
        var cpf = new Cpf("12345678909");

        // Act
        var result = cpf.ToString();

        // Assert
        Assert.Equal("123.456.789-09", result);
    }

    [Fact]
    public void Should_Be_Equal_For_Same_Cpf_Value_With_Different_Masks()
    {
        // Arrange
        var cpf1 = new Cpf("123.456.789-09"); // Masked format
        var cpf2 = new Cpf("12345678909");    // Unmasked format

        // Act & Assert
        Assert.Equal(cpf1, cpf2); // Should be equal because they represent the same CPF
    }

    [Fact]
    public void Should_Have_Same_HashCode_For_Same_Cpf_Value()
    {
        // Arrange
        var cpf1 = new Cpf("123.456.789-09"); // Masked format
        var cpf2 = new Cpf("12345678909");    // Unmasked format

        // Act & Assert
        Assert.Equal(cpf1.GetHashCode(), cpf2.GetHashCode()); // Same hash code for equivalent CPFs
    }

    [Fact]
    public void Should_Not_Be_Equal_For_Different_Cpf_Values()
    {
        // Arrange
        var cpf1 = new Cpf("12345678909");
        var cpf2 = new Cpf("98765432100");

        // Act & Assert
        Assert.NotEqual(cpf1, cpf2); // Different CPFs should not be equal
    }

    [Fact]
    public void Should_Throw_Exception_For_Empty_Cpf()
    {
        // Arrange
        var emptyCpf = "";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Cpf(emptyCpf));
    }
}