namespace Deputies.Application.Dtos
{
    public record DeputyExpenseAPIDto(
        int DeputyId,
        string DeputyName,
        decimal ExpenseValue,
        string ExpenseType,
        int Year,
        int Month
    );
}