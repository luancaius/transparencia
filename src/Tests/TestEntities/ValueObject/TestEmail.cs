using Entities.ValueObject;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestEntities.ValueObject;

[TestClass]
public class TestEmail
{
    [TestMethod]
    public void Email_NullOrWhitespace_ReturnsNoEmailProvidedMessage()
    {
        var email = new Email(null); // Testing with null
        Assert.AreEqual("No email provided.", email.ToString());

        email = new Email(""); // Testing with empty string
        Assert.AreEqual("No email provided.", email.ToString());

        email = new Email("   "); // Testing with whitespace
        Assert.AreEqual("No email provided.", email.ToString());
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Email_InvalidFormat_ThrowsArgumentException()
    {
        var email = new Email("invalid-email"); // An example of an invalid email format
    }

    [TestMethod]
    public void Email_ValidEmail_CreatesInstance()
    {
        var validEmail = "test@example.com";
        var email = new Email(validEmail);
        Assert.AreEqual(validEmail, email.ToString());
    }

    // Test to ensure that IsValidEmail method correctly identifies valid email formats
    [TestMethod]
    public void Email_ValidEmailWithComplexFormat_CreatesInstance()
    {
        var validEmail = "valid.email+alias@example.co.uk";
        var email = new Email(validEmail);
        Assert.AreEqual(validEmail, email.ToString());
    }

    // Test to ensure that IsValidEmail method correctly identifies invalid email formats
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Email_InvalidEmailWithMissingAtSymbol_ThrowsArgumentException()
    {
        var invalidEmail = "invalidemail.com";
        var email = new Email(invalidEmail);
    }

    // Test to ensure that IsValidEmail method correctly identifies invalid email formats
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Email_InvalidEmailWithMissingDomain_ThrowsArgumentException()
    {
        var invalidEmail = "invalidemail@";
        var email = new Email(invalidEmail);
    }
}