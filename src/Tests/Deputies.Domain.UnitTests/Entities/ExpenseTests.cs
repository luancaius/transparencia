using Deputies.Domain.Entities;

namespace Deputies.Domain.UnitTests.Entities;

public class ExpenseTests
{
    [Fact]
    public void Should_Create_Expense_With_Valid_Data()
    {
        // Arrange
        decimal amount = 100.50m;
        string description = "Office Supplies";
        DateTime date = DateTime.Now;
        // Act
        var expense = new Expense(amount, date, description);

        // Assert
        Assert.Equal(amount, expense.Amount);
        Assert.Equal(description, expense.Description);
    }
}