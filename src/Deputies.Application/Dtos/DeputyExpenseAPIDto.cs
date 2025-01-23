namespace Deputies.Application.Dtos;

public record DeputyExpenseAPIDto(
    string DeputyId,
    string DeputyName,
    decimal ExpenseValue,
    string ExpenseType,
    int Year,
    int Month
);