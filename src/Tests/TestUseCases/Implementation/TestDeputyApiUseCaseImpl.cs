using DeputyUseCase.Implementation;
using DTO.Layer_1_2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Repositories.Interfaces;

namespace TestUseCases.Implementation
{
    [TestClass]
    public class TestDeputyApiUseCaseImpl
    {
        private DeputyApiUseCaseImpl _deputyApiUseCaseImpl;
        private Mock<IExpenseRepository> _mockExpenseRepository;

        [TestInitialize]
        public void Setup()
        {
            _mockExpenseRepository = new Mock<IExpenseRepository>();
            _deputyApiUseCaseImpl = new DeputyApiUseCaseImpl(_mockExpenseRepository.Object);
        }

        [TestMethod]
        public async Task GetTop10ExpensesAsync_ShouldReturnExpensesByDateRange()
        {
            // Arrange
            var dateStart = new DateTime(2024, 10, 1);
            var dateEnd = new DateTime(2024, 10, 15);
            var mockExpenses = new List<Expense>
            {
                new Expense { Amount = 100, Date = new DateTime(2024, 10, 5) },
                new Expense { Amount = 200, Date = new DateTime(2024, 10, 10) }
            };
            _mockExpenseRepository.Setup(repo => repo.GetExpensesByDateRangeAsync(dateStart, dateEnd, 10))
                .ReturnsAsync(mockExpenses);

            // Act
            var result = await _deputyApiUseCaseImpl.GetTop10ExpensesAsync(dateStart, dateEnd);

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(100, result.First().Amount);
        }

        [TestMethod]
        public async Task GetTop10ExpensesAsync_ShouldReturnEmpty_WhenNoExpensesInRange()
        {
            // Arrange
            var dateStart = new DateTime(2024, 9, 1);
            var dateEnd = new DateTime(2024, 9, 10);
            _mockExpenseRepository.Setup(repo => repo.GetExpensesByDateRangeAsync(dateStart, dateEnd, 10))
                .ReturnsAsync(new List<Expense>());

            // Act
            var result = await _deputyApiUseCaseImpl.GetTop10ExpensesAsync(dateStart, dateEnd);

            // Assert
            Assert.AreEqual(0, result.Count);
        }
    }
}
