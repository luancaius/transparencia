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

    public async Task<List<Expense>> GetTop10ExpensesAsync(DateTime dateStart, DateTime dateEnd)
    {
        var expenses = await _expenseRepository.GetExpensesByDateRangeAsync(dateStart, dateEnd, LIMIT);
        return expenses;
    }
}