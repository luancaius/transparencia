using Deputies.Domain.Entities;
using Deputies.Domain.ValueObjects;

namespace Deputies.Domain.UnitTests.Entities;

public class ExpenseTests
{
    [Fact]
    public void Create_Expense_With_Valid_Data_Should_Succeed()
    {
        // Arrange
        var buyer = Person.Create(
            new Cpf("12345678900"),
            new PersonName("John Doe")     // Using PersonName VO for buyer
        );
            
        var supplier = Company.Create(
            new Cnpj("10217831000173"),
            "ACME Telecom"                // or new CompanyName("ACME Telecom") if you have that VO
        );

        decimal amount = 150.75m;
        DateTime date = DateTime.UtcNow.AddDays(-1); // Yesterday
        string description = "Internet Service";

        // Act
        var expense = new Expense(amount, date, description, buyer, supplier);

        // Assert
        Assert.Equal(amount, expense.Amount);
        Assert.Equal(date, expense.Date);
        Assert.Equal(description, expense.Description);
        Assert.Equal(buyer, expense.Buyer);
        Assert.Equal(supplier, expense.Supplier);
    }

    [Fact]
    public void Create_Expense_Should_Throw_When_Amount_Is_Negative()
    {
        // Arrange
        var buyer = Person.Create(
            new Cpf("12345678900"),
            new PersonName("John Doe")
        );

        var supplier = Company.Create(
            new Cnpj("10217831000173"),
            "ACME Telecom"
        );

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new Expense(
                amount: -10m,
                date: DateTime.UtcNow,
                description: "Invalid negative amount",
                buyer: buyer,
                supplier: supplier
            )
        );
    }

    [Fact]
    public void Create_Expense_Should_Throw_When_Date_Is_In_Future()
    {
        // Arrange
        var buyer = Person.Create(
            new Cpf("12345678900"),
            new PersonName("John Doe")
        );

        var supplier = Company.Create(
            new Cnpj("10217831000173"),
            "ACME Telecom"
        );

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new Expense(
                amount: 100m,
                date: DateTime.UtcNow.AddDays(1), // Tomorrow
                description: "Future date",
                buyer: buyer,
                supplier: supplier
            )
        );
    }

    [Fact]
    public void Create_Expense_Should_Throw_When_Description_Is_Null()
    {
        // Arrange
        var buyer = Person.Create(
            new Cpf("12345678900"),
            new PersonName("John Doe")
        );
        var supplier = Company.Create(
            new Cnpj("10217831000173"),
            "ACME Telecom"
        );

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() =>
            new Expense(
                amount: 100m,
                date: DateTime.UtcNow,
                description: null, // Null description
                buyer: buyer,
                supplier: supplier
            )
        );
    }

    [Fact]
    public void Create_Expense_Should_Throw_When_Description_Is_Empty()
    {
        // Arrange
        var buyer = Person.Create(
            new Cpf("12345678900"),
            new PersonName("John Doe")
        );
        var supplier = Company.Create(
            new Cnpj("10217831000173"),
            "ACME Telecom"
        );

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() =>
            new Expense(
                amount: 100m,
                date: DateTime.UtcNow,
                description: "", // Empty description
                buyer: buyer,
                supplier: supplier
            )
        );
    }

    [Fact]
    public void Create_Expense_Should_Throw_When_Buyer_Is_Null()
    {
        // Arrange
        var supplier = Company.Create(
            new Cnpj("10217831000173"),
            "ACME Telecom"
        );

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() =>
            new Expense(
                amount: 100m,
                date: DateTime.UtcNow,
                description: "Buyer is null",
                buyer: null, // Null buyer
                supplier: supplier
            )
        );
    }

    [Fact]
    public void Create_Expense_Should_Throw_When_Supplier_Is_Null()
    {
        // Arrange
        var buyer = Person.Create(
            new Cpf("12345678900"),
            new PersonName("John Doe")
        );

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() =>
            new Expense(
                amount: 100m,
                date: DateTime.UtcNow,
                description: "Supplier is null",
                buyer: buyer,
                supplier: null // Null supplier
            )
        );
    }
}