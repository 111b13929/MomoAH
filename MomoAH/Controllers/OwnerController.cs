using Microsoft.AspNetCore.Mvc;
using MomoAH.Interfaces;
using MomoAH.Models;

namespace MomoAH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private readonly IOwnerRepository _repository;

        public OwnerController(IOwnerRepository repository)
        {
            _repository = repository;
        }

        // 獲取所有飼主資料
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string keyword = "")
        {
            var owners = await _repository.GetAllOwnersAsync(keyword);
            return Ok(owners);
        }

        // 根據 ID 獲取飼主資料
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var owner = await _repository.GetOwnerByIdAsync(id);
                return Ok(owner);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Owner owner)
        {
            if (string.IsNullOrWhiteSpace(owner.Name) ||
                string.IsNullOrWhiteSpace(owner.Gender) ||
                string.IsNullOrWhiteSpace(owner.Phone))
            {
                return BadRequest("姓名、性別和電話是必填欄位！");
            }

            owner.OwnerId = Guid.NewGuid().ToString(); // 自動生成唯一 ID
            await _repository.AddOwnerAsync(owner);
            return CreatedAtAction(nameof(GetById), new { id = owner.OwnerId }, owner);
        }




        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] Owner owner)
        {
            if (id != owner.OwnerId)
            {
                return BadRequest("路徑參數 ID 與飼主 ID 不一致！");
            }

            try
            {
                await _repository.UpdateOwnerAsync(owner);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"更新失敗：{ex.Message}");
            }
        }


        // 刪除飼主資料
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _repository.DeleteOwnerAsync(id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
