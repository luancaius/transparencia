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

        var expensesTop10 = expenses.OrderByDescending(e => e.Amount).Take(10).ToList();
        var deputiesExpensesDto = new List<DeputyExpenseAPIDto>();
        foreach (var expense in expensesTop10)
        {
            var deputy = expense.Buyer as Deputy;
            deputiesExpensesDto.Add(new DeputyExpenseAPIDto(
                DeputyId: deputy?.MultiSourceId.Ids.GetValueOrDefault("CamaraApi") ?? "0",  
                DeputyName: deputy?.DeputyName ?? "Unknown Deputy",
                ExpenseValue: expense.Amount,
                ExpenseType: expense.Description,  
                Year: expense.Date.Year,
                Month: expense.Date.Month,
                UrlDocumento: expense.UrlDocument ?? string.Empty
            ));
        }
        
        return deputiesExpensesDto;
    }
}
