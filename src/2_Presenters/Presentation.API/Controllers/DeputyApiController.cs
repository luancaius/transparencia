using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DeputyUseCase.Implementation;
using DeputyUseCase.Interfaces;

namespace Presentation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeputyApiController : ControllerBase
    {
        private readonly IDeputyApiUseCase _deputyApiUseCase;

        public DeputyApiController(IDeputyApiUseCase deputyApiUseCase)
        {
            _deputyApiUseCase = deputyApiUseCase;
        }

        [HttpGet("top10")]
        public async Task<IActionResult> GetTop10Expenses([FromQuery] int? week, [FromQuery] int? month)
        {
            if (week == null && month == null)
            {
                return BadRequest("Please provide either week or month.");
            }

            var expenses = await _deputyApiUseCase.GetTop10ExpensesAsync(week, month);
            
            if (expenses == null || !expenses.Any())
            {
                return NotFound("No expenses found.");
            }

            return Ok(expenses);
        }
    }
}