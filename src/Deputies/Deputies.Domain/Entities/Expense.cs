using System;
using Deputies.Domain.AbstractEntities;

namespace Deputies.Domain.Entities;

public class Expense
{
    public string Description { get; private set; }
    public decimal Amount { get; private set; }
    public DateTime Date { get; private set; }
    public Participant Buyer { get; private set; }
    public Participant Supplier { get; private set; }

    public Expense(
        decimal amount,
        DateTime date,
        string description,
        Participant buyer,
        Participant supplier
    )
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount cannot be negative.");

        if (date > DateTime.UtcNow)
            throw new ArgumentOutOfRangeException(nameof(date), "Date cannot be in the future.");

        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentNullException(nameof(description), "Description cannot be empty.");

        Buyer = buyer ?? throw new ArgumentNullException(nameof(buyer), "Buyer cannot be null.");
        Supplier = supplier ?? throw new ArgumentNullException(nameof(supplier), "Supplier cannot be null.");

        Amount = amount;
        Date = date;
        Description = description;
    }
}