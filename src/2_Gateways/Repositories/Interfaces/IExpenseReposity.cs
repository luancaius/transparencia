using DTO.Layer_1_2;

namespace Repositories.Interfaces;

public interface IExpenseRepository
{
    Task<List<Expense>> GetExpensesByDateRangeAsync(DateTime dateStart, DateTime dateEnd, int limit);
}