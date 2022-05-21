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

        [HttpGet("GetCountries")]
        public async Task<IActionResult> GetCountries()
        {
            var result = await _filterGroupService.GetFilterGroupsAsync();
            return Ok(result);
        }
        [HttpGet("GetFilterGroupById/{filterGroupId}")]
        public async Task<IActionResult> GetFilterGroupById(int filterGroupId)
        {
            var result = await _filterGroupService.GetFilterGroupByIdAsync(filterGroupId);
            return Ok(result);
        }

        [HttpPost("CreateFilterGroup")]
        public async Task<IActionResult> CreateFilterGroup([FromBody] FilterGroupRequest request)
        {
            await _filterGroupService.CreateFilterGroupAsync(request);
            return Ok("Filter group created successfully");
        }

        [HttpPut("UpdateFilterGroup/{filterGroupId}")]
        public async Task<IActionResult> UpdateFilterGroup(int filterGroupId, [FromBody] FilterGroupRequest request)
        {
            await _filterGroupService.UpdateFilterGroupAsync(filterGroupId, request);
            return Ok("Filter group updated successfully");
        }

        [HttpDelete("DeleteFilterGroup/{filterGroupId}")]
        public async Task<IActionResult> DeleteFilterGroup(int filterGroupId)
        {
            await _filterGroupService.DeleteFilterGroupAsync(filterGroupId);
            return Ok("Filter group deleted successfully");
        }
    }
}
