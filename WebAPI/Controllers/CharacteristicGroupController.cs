using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebAPI.Interfaces;
using WebAPI.ViewModels.Request;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacteristicGroupController : ControllerBase
    {
        private string UserId => User?.FindFirstValue(ClaimTypes.NameIdentifier);

        private readonly ICharacteristicGroupService _characteristicGroupService;
        public CharacteristicGroupController(ICharacteristicGroupService characteristicGroupService)
        {
            _characteristicGroupService = characteristicGroupService;
        }

        [Authorize(Roles = "Admin,Seller")]
        [HttpGet("Get")]
        public async Task<IActionResult> Get()
        {
            var result = await _characteristicGroupService.GetAsync(UserId);
            return Ok(result);
        }

        [Authorize(Roles = "Admin,Seller")]
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _characteristicGroupService.GetByIdAsync(id);
            return Ok(result);
        }

        [Authorize(Roles = "Admin,Seller")]
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CharacteristicGroupRequest request)
        {
            await _characteristicGroupService.CreateAsync(request, UserId);
            return Ok("CharacteristicGroup updated successfully");
        }

        [Authorize(Roles = "Admin,Seller")]
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CharacteristicGroupRequest request)
        {
            await _characteristicGroupService.UpdateAsync(id, request);
            return Ok("CharacteristicGroup updated successfully");
        }

        [Authorize(Roles = "Admin,Seller")]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _characteristicGroupService.DeleteAsync(id);
            return Ok("CharacteristicGroup deleted successfully");
        }
    }
}
