using Repositories.DTO.ExposedApi;

namespace DeputyUseCase.DTO;

public class Expense
{
    public string DeputyName { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }

    public Expense(ExpenseRepo value)
    {
        DeputyName = value.DeputyName;
        Amount = value.Amount;
        Date = value.Date;
    }
}