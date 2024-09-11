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
        public async Task GetTop10ExpensesAsync_ShouldReturnExpensesByWeek()
        {
            // Arrange
            var week = 34;
            var mockExpenses = new List<Expense>
            {
                new Expense { Amount = 100 },
                new Expense { Amount = 200 }
            };
            _mockExpenseRepository.Setup(repo => repo.GetExpensesByWeekAsync(week, 10))
                .ReturnsAsync(mockExpenses);

            // Act
            var result = await _deputyApiUseCaseImpl.GetTop10ExpensesAsync(week, null);

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(100, result.First().Amount);
        }

        [TestMethod]
        public async Task GetTop10ExpensesAsync_ShouldReturnExpensesByMonth()
        {
            // Arrange
            var month = 8;
            var mockExpenses = new List<Expense>
            {
                new Expense { Amount = 300 },
                new Expense { Amount = 400 }
            };
            _mockExpenseRepository.Setup(repo => repo.GetExpensesByMonthAsync(month, 10))
                .ReturnsAsync(mockExpenses);

            // Act
            var result = await _deputyApiUseCaseImpl.GetTop10ExpensesAsync(null, month);

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(300, result.First().Amount);
        }
    }
}
