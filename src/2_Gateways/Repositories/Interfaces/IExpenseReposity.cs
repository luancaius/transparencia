using DTO.Layer_1_2;

namespace Repositories.Interfaces;

public interface IExpenseRepository
{
    Task<List<Expense>> GetExpensesByWeekAsync(int week, int limit);
    Task<List<Expense>> GetExpensesByMonthAsync(int month, int limit);
}
