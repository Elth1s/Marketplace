using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Interfaces;
using WebAPI.ViewModels.Request;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilterNameController : ControllerBase
    {
        private readonly IFilterNameService _filterNameService;
        public FilterNameController(IFilterNameService filterNameService)
        {
            _filterNameService = filterNameService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetFiltersName")]
        public async Task<IActionResult> GetFiltersName()
        {
            var result = await _filterNameService.GetFiltersNameAsync();
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetFilterNameById/{filterNameId}")]
        public async Task<IActionResult> GetFilterNameById(int filterNameId)
        {
            var result = await _filterNameService.GetFilterNameByIdAsync(filterNameId);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("CreateFilterName")]
        public async Task<IActionResult> CreateFilterName([FromBody] FilterNameRequest request)
        {
            await _filterNameService.CreateFilterNameAsync(request);
            return Ok("Filter name created successfully");
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateFilterName/{filterId}")]
        public async Task<IActionResult> UpdateFilterName(int filterNameId, [FromBody] FilterNameRequest request)
        {
            await _filterNameService.UpdateFilterNameAsync(filterNameId, request);
            return Ok("Filter name updated successfully");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteFilterName/{filterId}")]
        public async Task<IActionResult> DeleteFilterName(int filterNameId)
        {
            await _filterNameService.DeleteFilterNameAsync(filterNameId);
            return Ok("Filter name deleted successfully");
        }
    }
}
