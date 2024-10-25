using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> GetTop10Expenses([FromQuery] DateTime? dateStart, [FromQuery] DateTime? dateEnd)
        {
            if (dateStart == null || dateEnd == null)
            {
                return BadRequest("Please provide both dateStart and dateEnd.");
            }

            var expenses = await _deputyApiUseCase.GetTop10ExpensesAsync(dateStart.Value, dateEnd.Value);
    
            if (expenses == null || !expenses.Any())
            {
                return NotFound("No expenses found.");
            }

            return Ok(expenses);
        }

    }
}