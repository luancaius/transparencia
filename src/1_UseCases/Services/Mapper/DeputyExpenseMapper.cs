using Entities.Entities;
using RelationalDatabase.DTO.Deputado;
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
            expenseDto.CnpjOrCpf,
            expenseDto.NameCompany
        );

        return despesa;
    }
    
    public static DeputyExpense MapToDeputyExpense(DeputyExpenseDomain expenseDomain)
    {
        
        var despesa = new DeputyExpense
        {
            DateTimeExpense = expenseDomain.DateTimeExpense,
            DeputyId = expenseDomain.DeputyId,
            AmountDocument = expenseDomain.AmountDocument,
            AmountFinal = expenseDomain.AmountFinal,
            ReceiptUrl = expenseDomain.ReceiptUrl,
            TypeExpense = expenseDomain.TypeExpense,
            TypeReceipt = expenseDomain.TypeReceipt,
            NumberDocument = expenseDomain.NumberDocument,
            IdDocument = expenseDomain.IdDocument
        };
        
        return despesa;
    } 
    
}