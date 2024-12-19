using Microsoft.AspNetCore.Mvc;
using MomoAH.Interfaces;
using MomoAH.Models;

namespace MomoAH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorSpecialtyController : ControllerBase
    {
        private readonly IDoctorSpecialtyRepository _repository;

        public DoctorSpecialtyController(IDoctorSpecialtyRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var results = await _repository.GetAllAsync();
            return Ok(results);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] DoctorSpecialty doctorSpecialty)
        {
            await _repository.AddAsync(doctorSpecialty);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] string doctorId, [FromQuery] string specialtyId)
        {
            await _repository.DeleteAsync(doctorId, specialtyId);
            return NoContent();
        }
    }
}
