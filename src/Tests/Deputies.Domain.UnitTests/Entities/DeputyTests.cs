using Deputies.Domain.Entities;
using Deputies.Domain.ValueObjects;

namespace Deputies.Domain.UnitTests.Entities;

public class DeputyTests
{
    [Fact]
    public void Should_Create_Deputy_With_Valid_Data()
    {
        // Arrange
        var person = Person.Create(new Cpf("12345678909"), new Name("John", "Doe"));
        var multiSourceId = new MultiSourceId("API1", "12345");
        string deputyName = "John Doe";
        string party = "XYZ";

        // Act
        var deputy = Deputy.Create(person, deputyName, party, multiSourceId);

        // Assert
        Assert.Equal(person, deputy.Person);
        Assert.Equal(deputyName, deputy.DeputyName);
        Assert.Equal(party, deputy.Party);
        Assert.Equal(multiSourceId, deputy.MultiSourceId);
    }

    [Fact]
    public void Should_Throw_Exception_When_Person_Is_Null()
    {
        // Arrange
        var multiSourceId = new MultiSourceId("API1", "12345");
        string deputyName = "John Doe";
        string party = "XYZ";

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => Deputy.Create(null!, deputyName, party, multiSourceId));
    }

    [Fact]
    public void Should_Throw_Exception_When_DeputyName_Is_Empty()
    {
        // Arrange
        var person = Person.Create(new Cpf("98765432100"), new Name("John", "Doe"));
        var multiSourceId = new MultiSourceId("API1", "12345");
        string party = "XYZ";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => Deputy.Create(person, "", party, multiSourceId));
    }

    [Fact]
    public void Should_Throw_Exception_When_Party_Is_Empty()
    {
        // Arrange
        var person = Person.Create(new Cpf("11144477735"), new Name("John", "Doe"));
        var multiSourceId = new MultiSourceId("API1", "12345");
        string deputyName = "John Doe";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => Deputy.Create(person, deputyName, "", multiSourceId));
    }

    [Fact]
    public void Should_Throw_Exception_When_MultiSourceId_Is_Null()
    {
        // Arrange
        var person = Person.Create(new Cpf("52998224725"), new Name("John", "Doe"));
        string deputyName = "John Doe";
        string party = "XYZ";

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => Deputy.Create(person, deputyName, party, null!));
    }

    [Fact]
    public void Should_Return_True_For_Deputies_With_Same_Person()
    {
        // Arrange
        var person = Person.Create(new Cpf("01234567890"), new Name("John", "Doe"));
        var multiSourceId1 = new MultiSourceId("API1", "12345");
        var multiSourceId2 = new MultiSourceId("API2", "67890");

        var deputy1 = Deputy.Create(person, "John Doe", "XYZ", multiSourceId1);
        var deputy2 = Deputy.Create(person, "Another Name", "ABC", multiSourceId2);

        // Act & Assert
        Assert.Equal(deputy1, deputy2); // Should be equal because they share the same Person
    }

    [Fact]
    public void Should_Return_False_For_Deputies_With_Different_Person()
    {
        // Arrange
        var person1 = Person.Create(new Cpf("12345678909"), new Name("John", "Doe"));
        var person2 = Person.Create(new Cpf("98765432100"), new Name("Jane", "Smith"));
        var multiSourceId = new MultiSourceId("API1", "12345");

        var deputy1 = Deputy.Create(person1, "John Doe", "XYZ", multiSourceId);
        var deputy2 = Deputy.Create(person2, "Jane Smith", "ABC", multiSourceId);

        // Act & Assert
        Assert.NotEqual(deputy1, deputy2); // Should not be equal because they have different Person instances
    }

    [Fact]
    public void Should_Have_Same_HashCode_For_Deputies_With_Same_Person()
    {
        // Arrange
        var person = Person.Create(new Cpf("52998224725"), new Name("John", "Doe"));
        var multiSourceId1 = new MultiSourceId("API1", "12345");
        var multiSourceId2 = new MultiSourceId("API2", "67890");

        var deputy1 = Deputy.Create(person, "John Doe", "XYZ", multiSourceId1);
        var deputy2 = Deputy.Create(person, "Another Name", "ABC", multiSourceId2);

        // Act & Assert
        Assert.Equal(deputy1.GetHashCode(), deputy2.GetHashCode()); // Should have the same hash code because they share the same Person
    }

    [Fact]
    public void Should_Return_Formatted_String_From_ToString()
    {
        // Arrange
        var person = Person.Create(new Cpf("01234567890"), new Name("John", "Doe"));
        var multiSourceId = new MultiSourceId("API1", "12345");
        var deputy = Deputy.Create(person, "John Doe", "XYZ", multiSourceId);

        // Act
        var result = deputy.ToString();

        // Assert
        Assert.Equal("John Doe (XYZ) - API1: 12345", result);
    }
    
    [Fact]
    public void Should_Throw_Exception_For_Invalid_Cpf()
    {
        // Arrange
        var invalidCpf = "12345678900";
        var name = new Name("John", "Doe");

        // Act & Assert
        Assert.Throws<ArgumentException>(() => Person.Create(new Cpf(invalidCpf), name));
    }
}