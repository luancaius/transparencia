using DeputyUseCase.DTO;

namespace DeputyUseCase.Implementation;

public interface IDeputyApiUseCase
{
    Task<List<Expense>> GetTop10ExpensesAsync(int? week, int? month);
}