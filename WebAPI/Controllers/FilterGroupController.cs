using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Interfaces;
using WebAPI.ViewModels.Request;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilterGroupController : ControllerBase
    {
        private readonly IFilterGroupService _filterGroupService;
        public FilterGroupController(IFilterGroupService filterGroupService)
        {
            _filterGroupService = filterGroupService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetFilterGroups")]
        public async Task<IActionResult> GetFilterGroups()
        {
            var result = await _filterGroupService.GetFilterGroupsAsync();
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetFilterGroupById/{filterGroupId}")]
        public async Task<IActionResult> GetFilterGroupById(int filterGroupId)
        {
            var result = await _filterGroupService.GetFilterGroupByIdAsync(filterGroupId);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("CreateFilterGroup")]
        public async Task<IActionResult> CreateFilterGroup([FromBody] FilterGroupRequest request)
        {
            await _filterGroupService.CreateFilterGroupAsync(request);
            return Ok("Filter group created successfully");
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateFilterGroup/{filterGroupId}")]
        public async Task<IActionResult> UpdateFilterGroup(int filterGroupId, [FromBody] FilterGroupRequest request)
        {
            await _filterGroupService.UpdateFilterGroupAsync(filterGroupId, request);
            return Ok("Filter group updated successfully");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteFilterGroup/{filterGroupId}")]
        public async Task<IActionResult> DeleteFilterGroup(int filterGroupId)
        {
            await _filterGroupService.DeleteFilterGroupAsync(filterGroupId);
            return Ok("Filter group deleted successfully");
        }
    }
}
