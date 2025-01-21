using Deputies.Application.Dtos;
using Deputies.Application.Ports.In;
using Deputies.Application.Ports.Out;
using Deputies.Domain.Entities;

namespace Deputies.Application.Services;

public class DeputiesExpensesQueryService : IGetDeputiesExpensesQuery
{
    private readonly IDeputyRepository _deputiesRepository;
        
    public DeputiesExpensesQueryService(IDeputyRepository deputiesRepository)
    {
        _deputiesRepository = deputiesRepository;
    }

    public async Task<IEnumerable<DeputyExpenseAPIDto>> GetTop10ExpensesAsync(int year, int month)
    {
        List<Expense> expenses = await _deputiesRepository.GetExpensesByYearAndMonthAsync(year, month);

        return expenses
            .OrderByDescending(e => e.Amount)
            .Take(10)
            .Select(a => new DeputyExpenseAPIDto
            {
                DeputyId = a.,
                ExpenseId = a.ExpenseId,
                Amount = a.Amount,
                Date = a.Date,
                Description = a.Description
            })
            .ToList();
    }
}