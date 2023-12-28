using Entities.DomainEntities;
using Services.DTO.Deputy;

namespace Services.Mapper;

public static class DeputyExpenseMapper
{
    public static DeputyExpenseDomain MapToExpense(DeputyExpenseDto expenseDto)
    {
        var despesa = DeputyExpenseDomain.CreateExpense(
            Convert.ToDateTime(expenseDto.DateTimeExpenseStr),
            expenseDto.DeputyId,
            expenseDto.AmountDocument,
            expenseDto.AmountFinal,
            expenseDto.ReceiptUrl,
            expenseDto.TypeExpense,
            expenseDto.TypeReceipt,
            expenseDto.NumberDocument,
            expenseDto.IdDocument,
            expenseDto.Cnpj,
            expenseDto.NameCompany
        );

        return despesa;
    }
}