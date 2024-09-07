using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RelationalDatabase.Interfaces;
using Repositories.DTO.ExposedApi;
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

        public async Task<List<ExpenseRepo>> GetExpensesByWeekAsync(int week, int limit)
        {
            var startDate = FirstDateOfWeekISO8601(DateTime.Now.Year, week);
            var endDate = startDate.AddDays(7);

            // Fetch from the DeputyExpense table via DeputyExpenseRepository
            var deputyExpenses = _unitOfWork.DeputyExpenseRepository
                .GetAll(e => e.DateTimeExpense >= startDate && e.DateTimeExpense < endDate, 1, limit)
                .ToList();

            // Map DeputyExpense to ExpenseRepo DTO
            return deputyExpenses.Select(expense => new ExpenseRepo
            {
                Amount = expense.AmountFinal,
                Date = expense.DateTimeExpense ?? DateTime.MinValue
            }).ToList();
        }

        public async Task<List<ExpenseRepo>> GetExpensesByMonthAsync(int month, int limit)
        {
            var year = DateTime.Now.Year;
            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(1);

            // Fetch from the DeputyExpense table via DeputyExpenseRepository
            var deputyExpenses = _unitOfWork.DeputyExpenseRepository
                .GetAll(e => e.DateTimeExpense >= startDate && e.DateTimeExpense < endDate, 1, limit)
                .ToList();

            // Map DeputyExpense to ExpenseRepo DTO
            return deputyExpenses.Select(expense => new ExpenseRepo
            {
                Amount = expense.AmountFinal,
                Date = expense.DateTimeExpense ?? DateTime.MinValue
            }).ToList();
        }

        // Utility method to calculate the start date of the week
        private static DateTime FirstDateOfWeekISO8601(int year, int weekOfYear)
        {
            var jan1 = new DateTime(year, 1, 1);
            var daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;

            var firstThursday = jan1.AddDays(daysOffset);
            var cal = System.Globalization.CultureInfo.CurrentCulture.Calendar;
            var firstWeek = cal.GetWeekOfYear(firstThursday, System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            var weekNum = weekOfYear - (firstWeek <= 1 ? 1 : 0);
            return firstThursday.AddDays(weekNum * 7 - 3);
        }
    }
}
