using DeputyUseCase.DTO;

namespace DeputyUseCase.Interfaces;

public interface IDeputyApiUseCase
{
    Task<List<Expense>> GetTop10ExpensesAsync(int? week, int? month);
}