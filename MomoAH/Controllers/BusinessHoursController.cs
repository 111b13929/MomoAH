using Microsoft.AspNetCore.Mvc;
using MomoAH.Interfaces;
using MomoAH.Models;

namespace MomoAH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessHoursController : ControllerBase
    {
        private readonly IBusinessHoursRepository _repository;

        public BusinessHoursController(IBusinessHoursRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var results = await _repository.GetAllAsync();
            return Ok(results);
        }

        [HttpGet("{dayOfWeek}")]
        public async Task<IActionResult> GetByDay(string dayOfWeek)
        {
            var result = await _repository.GetByDayAsync(dayOfWeek);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] BusinessHours businessHours)
        {
            await _repository.UpdateAsync(businessHours);
            return NoContent();
        }
    }
}
