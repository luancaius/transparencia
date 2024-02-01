using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestEntities.ValueObject;

public class TestValueObject : global::Entities.ValueObject.ValueObject
{
    public string? Property1 { get; }
    public int Property2 { get; }

    public TestValueObject(string? property1, int property2)
    {
        Property1 = property1;
        Property2 = property2;
    }

    public override IEnumerable<object?> GetAtomicValues()
    {
        yield return Property1;
        yield return Property2;
    }
}

[TestClass]
public class TestValueObjectTest
{
    [TestMethod]
    public void Equals_GivenDifferentValueObjects_ReturnsFalse()
    {
        var object1 = new TestValueObject("Value1", 1);
        var object2 = new TestValueObject("Value2", 2);

        Assert.IsFalse(object1.Equals(object2));
    }

    [TestMethod]
    public void Equals_GivenIdenticalValueObjects_ReturnsTrue()
    {
        var object1 = new TestValueObject("Value", 1);
        var object2 = new TestValueObject("Value", 1);

        Assert.IsTrue(object1.Equals(object2));
    }

    [TestMethod]
    public void Equals_GivenNull_ReturnsFalse()
    {
        var object1 = new TestValueObject("Value", 1);

        Assert.IsFalse(object1.Equals(null));
    }

    [TestMethod]
    public void GetHashCode_GivenIdenticalValueObjects_ReturnsSameHashCode()
    {
        var object1 = new TestValueObject("Value", 1);
        var object2 = new TestValueObject("Value", 1);

        Assert.AreEqual(object1.GetHashCode(), object2.GetHashCode());
    }

    [TestMethod]
    public void GetHashCode_GivenDifferentValueObjects_ReturnsDifferentHashCodes()
    {
        var object1 = new TestValueObject("Value1", 1);
        var object2 = new TestValueObject("Value2", 2);

        Assert.AreNotEqual(object1.GetHashCode(), object2.GetHashCode());
    }
}