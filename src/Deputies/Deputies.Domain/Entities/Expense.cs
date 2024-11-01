namespace Deputies.Domain.Entities
{
    public class Expense
    {
        public string Description { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime Date { get; private set; }
        public Expense(string description, decimal amount, DateTime date)
        {
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Amount = amount;
            Date = date;
        }
    }
}