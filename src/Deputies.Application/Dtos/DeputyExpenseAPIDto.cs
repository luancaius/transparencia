namespace Deputies.Application.Dtos;

public record DeputyExpenseAPIDto(
    string DeputyId,
    string DeputyName,
    decimal ExpenseValue,
    string ExpenseType,
    string UrlDocumento,
    int Year,
    int Month
);