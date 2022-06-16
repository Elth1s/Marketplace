using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Interfaces;
using WebAPI.ViewModels.Request;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilterValueController : ControllerBase
    {
        private readonly IFilterValueService _filterValueService;
        public FilterValueController(IFilterValueService filterValueService)
        {
            _filterValueService = filterValueService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetFiltersValue")]
        public async Task<IActionResult> GetFiltersValue()
        {
            var result = await _filterValueService.GetFiltersValueAsync();
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetFilterValueById/{filterValueId}")]
        public async Task<IActionResult> GetFilterValueById(int filterValueId)
        {
            var result = await _filterValueService.GetFilterValueByIdAsync(filterValueId);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("CreateFilterValue")]
        public async Task<IActionResult> CreateFilterValue([FromBody] FilterValueRequest request)
        {
            await _filterValueService.CreateFilterValueAsync(request);
            return Ok("Filter value created successfully");
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateFilterValue/{filterId}")]
        public async Task<IActionResult> UpdateFilterValue(int filterValueId, [FromBody] FilterValueRequest request)
        {
            await _filterValueService.UpdateFilterValueAsync(filterValueId, request);
            return Ok("Filter value updated successfully");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteFilterValue/{filterId}")]
        public async Task<IActionResult> DeleteFilterValue(int filterValueId)
        {
            await _filterValueService.DeleteFilterValueAsync(filterValueId);
            return Ok("Filter value deleted successfully");
        }
    }
}
