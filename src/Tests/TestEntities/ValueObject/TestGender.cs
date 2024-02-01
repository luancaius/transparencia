using Entities.ValueObject;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestEntities.ValueObject;

[TestClass]
public class TestGender
{
    [DataTestMethod]
    [DataRow("M", Gender.Male)]
    [DataRow("F", Gender.Female)]
    [DataRow("male", Gender.Male)]
    [DataRow("female", Gender.Female)]
    [DataRow("Other", Gender.Other)]
    [DataRow("Unknown", Gender.Unknown)]
    public void FromString_ValidGenderString_ReturnsExpectedGender(string genderString, Gender expectedGender)
    {
        var result = GenderExtensions.FromString(genderString);
        Assert.AreEqual(expectedGender, result);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void FromString_NullOrWhiteSpace_ThrowsArgumentException()
    {
        GenderExtensions.FromString(null);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException), "Gender string cannot be null or whitespace.")]
    public void FromString_EmptyString_ThrowsArgumentException()
    {
        GenderExtensions.FromString(string.Empty);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException), "Gender string cannot be null or whitespace.")]
    public void FromString_WhiteSpace_ThrowsArgumentException()
    {
        GenderExtensions.FromString(" ");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void FromString_InvalidGenderString_ThrowsArgumentException()
    {
        GenderExtensions.FromString("InvalidGender");
    }
}