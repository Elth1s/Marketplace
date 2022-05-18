using Microsoft.AspNetCore.Mvc;
using WebAPI.ViewModels.Request;

namespace WebAPI.Interfaces
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacteristicController : Controller
    {
        private readonly ICharacteristicService _characteristicService;

        public CharacteristicController(ICharacteristicService characteristicService)
        {
            _characteristicService = characteristicService;
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get()
        {
            var result = await _characteristicService.GetAsync();
            return Ok(result);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _characteristicService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CharacteristicRequest request)
        {
            await _characteristicService.CreateAsync(request);
            return Ok("Characteristic updated successfully");
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CharacteristicRequest request)
        {
            await _characteristicService.UpdateAsync(id, request);
            return Ok("Characteristic updated successfully");
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _characteristicService.DeleteAsync(id);
            return Ok("Characteristic deleted successfully");
        }
    }
}
