using DeputyUseCase.Interfaces;
using DTO.Layer_1_2;
using Repositories.Interfaces;

namespace DeputyUseCase.Implementation;

public class DeputyApiUseCaseImpl : IDeputyApiUseCase
{
    private readonly IExpenseRepository _expenseRepository;
    private const int LIMIT = 10;
    
    public DeputyApiUseCaseImpl(IExpenseRepository expenseRepository)
    {
        _expenseRepository = expenseRepository;
    }

    public async Task<List<Expense>> GetTop10ExpensesAsync(int? week, int? month)
    {
        var expenses = new List<Expense>();

        if (week.HasValue)
        {
            expenses = await _expenseRepository.GetExpensesByWeekAsync(week.Value, LIMIT);
        }
        else if (month.HasValue)
        {
            expenses = await _expenseRepository.GetExpensesByMonthAsync(month.Value, LIMIT);
        }

        return expenses;
    }
}