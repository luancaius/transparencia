using Deputies.Application.Dtos;
using Deputies.Application.Ports.In;
using Deputies.Application.Ports.Out;

namespace Deputies.Application.Services
{
    public class DeputiesExpensesQueryService : IGetDeputiesExpensesQuery
    {
        private readonly IDeputyRepository _deputiesRepository;
        
        public DeputiesExpensesQueryService(IDeputyRepository deputiesRepository)
        {
            _deputiesRepository = deputiesRepository;
        }

        public async Task<IEnumerable<DeputyExpenseAPIDto>> GetTop10ExpensesAsync(int year, int month)
        {
            var expenses = await _deputiesRepository.GetExpensesByYearAndMonthAsync(year, month);

            return expenses
                .OrderByDescending(e => e.Amount)
                .Take(10)
                .Select(e => new DeputyExpenseDto(
                    e.DeputyId,
                    e.DeputyName,
                    e.Amount,
                    e.ExpenseType,
                    e.Year,
                    e.Month
                ))
                .ToList();
        }
    }
}