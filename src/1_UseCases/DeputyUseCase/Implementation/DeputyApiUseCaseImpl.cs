using DeputyUseCase.DTO;
using Gateways.Interfaces;
using Repositories.Interfaces;

namespace DeputyUseCase.Implementation;

public class DeputyApiUseCaseImpl : IDeputyApiUseCase
{
    private readonly IDeputiesGateway _deputiesGateway;
    private readonly IExpenseRepository _expenseRepository;

    public async Task<List<Expense>> GetTop10ExpensesAsync(int? week, int? month)
    {
        var expenses = new List<Repositories.DTO.ExposedApi.Expense>();

        if (week.HasValue)
        {
            expenses = await _expenseRepository.GetExpensesByWeekAsync(week.Value);
        }
        else if (month.HasValue)
        {
            expenses = await _expenseRepository.GetExpensesByMonthAsync(month.Value);
        }

        return null;
        // var expenseUseCase = //expenses.Select(a => )
        //
        // return expenses
        //     .OrderByDescending(e => e.Amount)  // Sort by amount in descending order
        //     .Take(10)  // Take the top 10
        //     .ToList();    
    }
}