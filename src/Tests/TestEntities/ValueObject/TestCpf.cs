using Entities.ValueObject;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestEntities.ValueObject;

[TestClass]
public class TestCpf
{
    [TestMethod]
    [ExpectedException(typeof(ArgumentException), "CPF cannot be null or empty.")]
    public void Cpf_NullOrEmpty_ThrowsArgumentException()
    {
        var cpf = new Cpf(null); // Test with null
        // You can also repeat this with an empty string to ensure that case is also handled.
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException), "Invalid CPF.")]
    public void Cpf_InvalidFormat_ThrowsArgumentException()
    {
        var cpf = new Cpf("111.111.111-11"); // An example of an invalid CPF due to identical digits
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException), "Invalid CPF.")]
    public void Cpf_InvalidCheckDigits_ThrowsArgumentException()
    {
        var cpf = new Cpf("123.456.789-99"); // Incorrect check digits
    }

    [TestMethod]
    public void Cpf_ValidCpf_CreatesInstance()
    {
        var validCpf = "131.145.025-47";
        var cpf = new Cpf(validCpf);
        Assert.AreEqual(validCpf, cpf.ToString());
    }
}