using Entities.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestEntities.Entities;

[TestClass]
public class TestCompanyDomain
{
    [TestMethod]
    public void CreateCompany_WithValidInputs_ShouldCreateCompany()
    {
        // Arrange
        string expectedName = "Test Company";
        string expectedCnpj = "33.113.309/0001-47";

        // Act
        var company = CompanyDomain.CreateCompany(expectedName, expectedCnpj);

        // Assert
        Assert.IsNotNull(company);
        Assert.AreEqual(expectedName, company.Name);
        Assert.IsNotNull(company.Cnpj);
        Assert.AreEqual(expectedCnpj, company.Cnpj.ToString());
    }
}