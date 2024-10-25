using DTO.Layer_1_2;
using RelationalDatabase.Interfaces;
using Repositories.Interfaces;

namespace Repositories.Implementation
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public ExpenseRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Expense>> GetExpensesByDateRangeAsync(DateTime dateStart, DateTime dateEnd, int limit)
        {
            // Fetch from the DeputyExpense table via DeputyExpenseRepository
            var deputyExpenses = _unitOfWork.DeputyExpenseRepository
                .GetAll(e => e.DateTimeExpense >= dateStart && e.DateTimeExpense <= dateEnd, 1, limit)
                .ToList();

            // Map DeputyExpense to ExpenseRepo DTO
            return deputyExpenses.Select(expense => new Expense
            {
                Amount = expense.AmountFinal,
                Date = expense.DateTimeExpense ?? DateTime.MinValue
            }).ToList();
        }
    }
}
