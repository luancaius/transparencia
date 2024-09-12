using Entities.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestEntities.Entities
{
    [TestClass]
    public class DeputyExpenseDomainTests
    {
        [TestMethod]
        public void CreateExpense_WithAllParameters_ReturnsValidObject()
        {
            // Arrange
            DateTime testDate = new DateTime(2022, 1, 1);
            int deputyId = 1;
            decimal amountDocument = 100m;
            decimal amountFinal = 90m;
            string receiptUrl = "http://example.com/receipt";
            string typeExpense = "Travel";
            string typeReceipt = "Electronic";
            string numberDocument = "123456";
            string idDocument = "ABC-123";
            string cnpjCompany = "35.517.653/0001-27";
            string cnpjCompanyFiltered = "35517653000127";
            string nameCompany = "Test Company";

            // Act
            var expense = DeputyExpenseDomain.CreateExpense(testDate, deputyId, amountDocument, amountFinal, receiptUrl,
                typeExpense, typeReceipt, numberDocument, idDocument, cnpjCompany, nameCompany);

            // Assert
            Assert.IsNotNull(expense);
            Assert.AreEqual(testDate, expense.DateTimeExpense);
            Assert.AreEqual(deputyId, expense.DeputyId);
            Assert.AreEqual(amountDocument, expense.AmountDocument);
            Assert.AreEqual(amountFinal, expense.AmountFinal);
            Assert.AreEqual(receiptUrl, expense.ReceiptUrl);
            Assert.AreEqual(typeExpense, expense.TypeExpense);
            Assert.AreEqual(typeReceipt, expense.TypeReceipt);
            Assert.AreEqual(numberDocument, expense.NumberDocument);
            Assert.AreEqual(idDocument, expense.IdDocument);
            Assert.IsNotNull(expense.Supplier);
            Assert.AreEqual(nameCompany, expense.Supplier.Name);
            Assert.AreEqual(cnpjCompanyFiltered, expense.Supplier.Cnpj.ToString());
        }

        [TestMethod]
        public void CreateExpense_WithNullReceiptUrl_UsesDefaultReceiptUrl()
        {
            // Arrange
            string defaultReceiptUrl = "Sem nota fiscal"; // Default value when receiptUrl is null

            // Act
            var expense = DeputyExpenseDomain.CreateExpense(DateTime.Now, 1, 100m, 90m, null,
                "Office", "Paper", "123456", "ABC-123", "35.517.653/0001-27", "Test Company");

            // Assert
            Assert.AreEqual(defaultReceiptUrl, expense.ReceiptUrl);
        }
    }
}
