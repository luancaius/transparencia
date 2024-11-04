namespace Deputies.Domain.Entities;

public class Expense
{
    public string Description { get; private set; }
    public decimal Amount { get; private set; }
    public DateTime Date { get; private set; }

    public Expense(decimal amount, DateTime date, string description)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount cannot be negative.");
        
        if (date > DateTime.UtcNow)
            throw new ArgumentOutOfRangeException(nameof(date), "Date cannot be in the future.");
        
        Description = description ?? throw new ArgumentNullException(nameof(description));
        Amount = amount;
        Date = date;
    }
}