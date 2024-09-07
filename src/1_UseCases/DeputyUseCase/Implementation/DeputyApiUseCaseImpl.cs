using DeputyUseCase.DTO;
using DeputyUseCase.Implementation;
using Repositories.DTO.ExposedApi;
using Repositories.Interfaces;

public class DeputyApiUseCaseImpl : IDeputyApiUseCase
{
    private readonly IExpenseRepository _expenseRepository;

    public DeputyApiUseCaseImpl(IExpenseRepository expenseRepository)
    {
        _expenseRepository = expenseRepository;
    }

    public async Task<List<Expense>> GetTop10ExpensesAsync(int? week, int? month)
    {
        var expenses = new List<ExpenseRepo>();

        if (week.HasValue)
        {
            expenses = await _expenseRepository.GetExpensesByWeekAsync(week.Value, 10);
        }
        else if (month.HasValue)
        {
            expenses = await _expenseRepository.GetExpensesByMonthAsync(month.Value, 10);
        }

        return expenses.Select(expenseRepo => new Expense(expenseRepo)).ToList();
    }
}