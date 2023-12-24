namespace Entities.DomainEntities;

public class ExpenseDomain
{
    public string Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime DateIncurred { get; set; }
    public string ReceiptUrl { get; set; }
    public int DeputyId { get; set; }
}