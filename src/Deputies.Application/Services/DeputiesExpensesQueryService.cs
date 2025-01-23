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
        
        var expensesDto = expenses
            .OrderByDescending(e => e.Amount)
            .Take(10)
            .Select(e =>
            {
                var deputy = e.Buyer as Deputy;
                return new DeputyExpenseAPIDto(
                    DeputyId: deputy?.MultiSourceId.Ids.GetValueOrDefault("CamaraApi") ?? "0",  
                    DeputyName: deputy?.DeputyName ?? "Unknown Deputy",
                    ExpenseValue: e.Amount,
                    ExpenseType: e.Description,  
                    Year: e.Date.Year,
                    Month: e.Date.Month
                );
            })
            .ToList();
        return expensesDto;
    }
}
