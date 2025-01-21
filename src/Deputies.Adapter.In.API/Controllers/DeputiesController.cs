using Deputies.Application.Ports.In;
using Microsoft.AspNetCore.Mvc;

namespace Deputies.Adapter.In.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DeputiesController : ControllerBase
{
    private readonly IGetDeputiesExpensesQuery _expensesQuery;

    public DeputiesController(IGetDeputiesExpensesQuery expensesQuery)
    {
        _expensesQuery = expensesQuery;
    }

    [HttpGet("{year}/{month}/top-expenses")]
    public async Task<IActionResult> GetTopExpenses(int year, int month)
    {
        var topExpenses = await _expensesQuery.GetTop10ExpensesAsync(year, month);
        return Ok(topExpenses);
    }
}