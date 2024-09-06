using Repositories.DTO.ExposedApi;

namespace Repositories.Interfaces;

public interface IExpenseRepository
{
    Task<List<Expense>> GetExpensesByWeekAsync(int week);
    Task<List<Expense>> GetExpensesByMonthAsync(int month);
}
