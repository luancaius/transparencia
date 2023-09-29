using NUnit.Framework;
using Entities.ValueObject;

namespace TestDomain.Entities.ValueObjects;

[TestFixture]
public class TestAddress
{
    [Test]
    public void Address_Constructor_Sets_Properties_Correctly()
    {
        // Arrange
        string street = "123 Main St";
        string city = "Anytown";
        string state = "CA";
        string country = "USA";
        string zipCode = "12345";

        // Act
        Address address = new Address(street, city, state, country, zipCode);

        // Assert
        Assert.AreEqual(street, address.Street);
        Assert.AreEqual(city, address.City);
        Assert.AreEqual(state, address.State);
        Assert.AreEqual(country, address.Country);
        Assert.AreEqual(zipCode, address.ZipCode);
    }

    [Test]
    public void GetAtomicValues_Returns_Correct_Values()
    {
        // Arrange
        string street = "123 Main St";
        string city = "Anytown";
        string state = "CA";
        string country = "USA";
        string zipCode = "12345";
        Address address = new Address(street, city, state, country, zipCode);

        // Act
        IEnumerable<object?> atomicValues = address.GetAtomicValues();

        // Assert
        Assert.AreEqual(street, atomicValues.ElementAt(0));
        Assert.AreEqual(city, atomicValues.ElementAt(1));
        Assert.AreEqual(state, atomicValues.ElementAt(2));
        Assert.AreEqual(country, atomicValues.ElementAt(3));
        Assert.AreEqual(zipCode, atomicValues.ElementAt(4));
    }
}

