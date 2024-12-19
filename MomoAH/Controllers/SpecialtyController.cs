using Microsoft.AspNetCore.Mvc;
using MomoAH.Interfaces;
using MomoAH.Models;

namespace MomoAH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialtyController : ControllerBase
    {
        private readonly ISpecialtyRepository _repository;

        public SpecialtyController(ISpecialtyRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var specialties = await _repository.GetAllAsync();
            return Ok(specialties);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var specialty = await _repository.GetByIdAsync(id);
                return Ok(specialty);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Specialty specialty)
        {
            await _repository.AddAsync(specialty);
            return CreatedAtAction(nameof(GetById), new { id = specialty.SpecialtyId }, specialty);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] Specialty specialty)
        {
            if (id != specialty.SpecialtyId) return BadRequest("ID 與 SpecialtyId 不一致");

            try
            {
                await _repository.UpdateAsync(specialty);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _repository.DeleteAsync(id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
