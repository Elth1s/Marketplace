using Microsoft.AspNetCore.Mvc;
using WebAPI.Interfaces;
using WebAPI.ViewModels.Request;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacteristicGroupController : ControllerBase
    {
        private readonly ICharacteristicGroupService _characteristicGroupService;
        public CharacteristicGroupController(ICharacteristicGroupService characteristicGroupService)
        {
            _characteristicGroupService = characteristicGroupService;
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get()
        {
            var result = await _characteristicGroupService.GetAsync();
            return Ok(result);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _characteristicGroupService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CharacteristicGroupRequest request)
        {
            await _characteristicGroupService.CreateAsync(request);
            return Ok("CharacteristicGroup updated successfully");
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CharacteristicGroupRequest request)
        {
            await _characteristicGroupService.UpdateAsync(id, request);
            return Ok("CharacteristicGroup updated successfully");
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _characteristicGroupService.DeleteAsync(id);
            return Ok("CharacteristicGroup deleted successfully");
        }
    }
}
