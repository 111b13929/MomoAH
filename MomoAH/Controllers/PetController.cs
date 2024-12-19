using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MomoAH.Interfaces;
using MomoAH.Models;

namespace MomoAH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private readonly IPetRepository _repository;

        public PetController(IPetRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var pets = await _repository.GetAllAsync();
            return Ok(pets);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var pet = await _repository.GetByIdAsync(id);
                return Ok(pet);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Pet pet)
        {
            if (string.IsNullOrEmpty(pet.PetId))
            {
                pet.PetId = Guid.NewGuid().ToString(); // 後端補生成 UUID
            }

            try
            {
                await _repository.AddAsync(pet);
                return CreatedAtAction(nameof(GetById), new { id = pet.PetId }, pet);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"發生錯誤: {ex.Message}");
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] Pet pet)
        {
            if (id != pet.PetId) return BadRequest("ID 與 PetId 不一致");
            if (pet == null) return BadRequest("資料不完整");

            await _repository.UpdateAsync(pet);
            return NoContent();
        }






        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _repository.DeleteAsync(id);
                return Ok(new { message = "刪除成功" });
            }
            catch (SqlException ex)
            {
                return Conflict(new { message = "無法刪除，因為該寵物資料與其他記錄存在關聯。" });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("ByOwner/{ownerId}")]
        public async Task<IActionResult> GetByOwnerId(string ownerId)
        {
            var pets = await _repository.GetByOwnerIdAsync(ownerId);
            return Ok(pets);
        }


    }
}
