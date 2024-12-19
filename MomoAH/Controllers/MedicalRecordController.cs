using Microsoft.AspNetCore.Mvc;
using MomoAH.Interfaces;
using MomoAH.Models;

namespace MomoAH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalRecordController : ControllerBase
    {
        private readonly IMedicalRecordRepository _repository;

        public MedicalRecordController(IMedicalRecordRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var records = await _repository.GetAllAsync();
            return Ok(records);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var record = await _repository.GetByIdAsync(id);
                return Ok(record);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] MedicalRecord record)
        {
            await _repository.AddAsync(record);
            return CreatedAtAction(nameof(GetById), new { id = record.RecordId }, record);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] MedicalRecord record)
        {
            if (id != record.RecordId) return BadRequest("ID 與 RecordId 不一致");

            try
            {
                await _repository.UpdateAsync(record);
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
