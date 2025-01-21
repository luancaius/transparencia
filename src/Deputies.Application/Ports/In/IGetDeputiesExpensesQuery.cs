using Deputies.Application.Dtos;

namespace Deputies.Application.Ports.In
{
    /// <summary>
    /// Provides read-only operations for Deputy expenses queries.
    /// </summary>
    public interface IGetDeputiesExpensesQuery
    {
        /// <summary>
        /// Returns the top 10 expenses for the specified year and month.
        /// </summary>
        /// <param name="year">The year to filter expenses.</param>
        /// <param name="month">The month to filter expenses.</param>
        /// <returns>A collection of at most 10 DeputyExpenseDto objects.</returns>
        Task<IEnumerable<DeputyExpenseAPIDto>> GetTop10ExpensesAsync(int year, int month);
    }
}