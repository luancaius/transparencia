using NUnit.Framework;

namespace TestDomain.Entities.ValueObjects;

[TestFixture]
public class ValueObjectTests
{
    [Test]
    public void ValueObject_EqualityAndHashcode_BehavesCorrectly()
    {
        // Arrange
        var valueObject1 = new TestValueObject("Test", 123);
        var valueObject2 = new TestValueObject("Test", 123);
        var valueObject3 = new TestValueObject("Different", 456);

        // Act & Assert
        // Test Equals method for equivalent objects
        Assert.IsTrue(valueObject1.Equals(valueObject2));
        Assert.IsTrue(valueObject2.Equals(valueObject1));

        // Test Equals method for different objects
        Assert.IsFalse(valueObject1.Equals(valueObject3));
        Assert.IsFalse(valueObject3.Equals(valueObject1));

        // Test GetHashCode method for equivalent objects
        Assert.AreEqual(valueObject1.GetHashCode(), valueObject2.GetHashCode());

        // Test GetHashCode method for different objects
        Assert.AreNotEqual(valueObject1.GetHashCode(), valueObject3.GetHashCode());
    }

    private class TestValueObject : ValueObjectTests
    {
        public string? Prop1 { get; }
        public int? Prop2 { get; }

        public TestValueObject(string? prop1, int? prop2)
        {
            Prop1 = prop1;
            Prop2 = prop2;
        }

        protected IEnumerable<object?> GetAtomicValues()
        {
            yield return Prop1;
            yield return Prop2;
        }
    }
}