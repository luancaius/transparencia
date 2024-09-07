using Repositories.DTO.ExposedApi;

namespace Repositories.Interfaces;

public interface IExpenseRepository
{
    Task<List<ExpenseRepo>> GetExpensesByWeekAsync(int week, int limit);
    Task<List<ExpenseRepo>> GetExpensesByMonthAsync(int month, int limit);
}
