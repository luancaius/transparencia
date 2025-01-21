using Deputies.Domain.Entities;
using Deputies.Domain.ValueObjects;
using Xunit;

namespace Deputies.Domain.UnitTests.Entities;

public class DeputyTests
{
    [Fact]
    public void Should_Create_Deputy_With_Valid_Data()
    {
        // Arrange
        var cpf = new Cpf("12345678909");
        var name = new PersonName("John", "Doe");
        var party = "XYZ";
        var multiSourceId = new MultiSourceId("API1", "12345");

        // Act
        var deputy = Deputy.Create(cpf, name, party, multiSourceId);

        // Assert
        Assert.Equal("John Doe", deputy.DisplayName);
        Assert.Equal(party, deputy.Party);
        Assert.Equal(multiSourceId, deputy.MultiSourceId);
    }

    [Fact]
    public void Should_Throw_Exception_When_Cpf_Is_Null()
    {
        // Arrange
        Cpf cpf = null!;
        var name = new PersonName("John", "Doe");
        var party = "XYZ";
        var multiSourceId = new MultiSourceId("API1", "12345");

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => 
            Deputy.Create(cpf, name, party, multiSourceId)
        );
    }

    [Fact]
    public void Should_Throw_Exception_When_PersonName_Is_Null()
    {
        // Arrange
        var cpf = new Cpf("12345678909");
        PersonName personName = null!;
        var party = "XYZ";
        var multiSourceId = new MultiSourceId("API1", "12345");

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() =>
            Deputy.Create(cpf, personName, party, multiSourceId)
        );
    }

    [Fact]
    public void Should_Throw_Exception_When_Party_Is_Empty()
    {
        // Arrange
        var cpf = new Cpf("12345678909");
        var name = new PersonName("John", "Doe");
        var multiSourceId = new MultiSourceId("API1", "12345");

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            Deputy.Create(cpf, name, "", multiSourceId)
        );
    }

    [Fact]
    public void Should_Throw_Exception_When_MultiSourceId_Is_Null()
    {
        // Arrange
        var cpf = new Cpf("12345678909");
        var name = new PersonName("John", "Doe");
        var party = "XYZ";
        MultiSourceId multiSourceId = null!;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() =>
            Deputy.Create(cpf, name, party, multiSourceId)
        );
    }

    [Fact]
    public void Should_Return_True_For_Deputies_With_Same_Cpf()
    {
        // Arrange
        var cpf = new Cpf("01234567890");
        var name1 = new PersonName("John", "Doe");
        var name2 = new PersonName("Another", "Name");
        var multiSourceId1 = new MultiSourceId("API1", "12345");
        var multiSourceId2 = new MultiSourceId("API2", "67890");

        var deputy1 = Deputy.Create(cpf, name1, "XYZ", multiSourceId1);
        var deputy2 = Deputy.Create(cpf, name2, "ABC", multiSourceId2);

        // Act & Assert
        Assert.Equal(deputy1, deputy2);
    }

    [Fact]
    public void Should_Return_False_For_Deputies_With_Different_Cpf()
    {
        // Arrange
        var cpf1 = new Cpf("12345678909");
        var cpf2 = new Cpf("98765432100");
        var name = new PersonName("John", "Doe");
        var multiSourceId = new MultiSourceId("API1", "12345");

        var deputy1 = Deputy.Create(cpf1, name, "XYZ", multiSourceId);
        var deputy2 = Deputy.Create(cpf2, name, "ABC", multiSourceId);

        // Act & Assert
        Assert.NotEqual(deputy1, deputy2);
    }

    [Fact]
    public void Should_Have_Same_HashCode_For_Deputies_With_Same_Cpf()
    {
        // Arrange
        var cpf = new Cpf("52998224725");
        var name1 = new PersonName("John", "Doe");
        var name2 = new PersonName("Another", "Name");
        var multiSourceId1 = new MultiSourceId("API1", "12345");
        var multiSourceId2 = new MultiSourceId("API2", "67890");

        var deputy1 = Deputy.Create(cpf, name1, "XYZ", multiSourceId1);
        var deputy2 = Deputy.Create(cpf, name2, "ABC", multiSourceId2);

        // Act & Assert
        Assert.Equal(deputy1.GetHashCode(), deputy2.GetHashCode());
    }

    [Fact]
    public void Should_Return_Formatted_String_From_ToString()
    {
        // Arrange
        var cpf = new Cpf("01234567890");
        var name = new PersonName("John", "Doe");
        var multiSourceId = new MultiSourceId("API1", "12345");
        var deputy = Deputy.Create(cpf, name, "XYZ", multiSourceId);

        // Act
        var result = deputy.ToString();

        // Assert
        Assert.Equal("John Doe (XYZ) - API1: 12345", result);
    }

    [Fact]
    public void Should_Throw_Exception_For_Invalid_Cpf()
    {
        // Arrange
        var invalidCpf = "12345678900"; // assume it's invalid in your Cpf validation
        var name = new PersonName("John", "Doe");
        var party = "XYZ";
        var multiSourceId = new MultiSourceId("API1", "12345");

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            Deputy.Create(new Cpf(invalidCpf), name, party, multiSourceId)
        );
    }
}
