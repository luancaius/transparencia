using Deputies.Domain.Entities;

namespace Deputies.Application.Ports.Out;

public interface IDeputyRepository
{
    Task SaveDeputyAsync(Deputy deputy);
    Task<Deputy?> GetDeputyByIdAsync(int deputyId);
    Task<List<Deputy>> GetDeputiesAsync();
    Task SaveExpensesAsync(String deputyId, List<Expense> expenses);
    Task<List<Expense>> GetExpensesByYearAndMonthAsync(int year, int month, int top = 10);
}