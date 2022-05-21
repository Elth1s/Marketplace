using Microsoft.AspNetCore.Mvc;
using WebAPI.Interfaces;
using WebAPI.ViewModels.Request;

namespace WebAPI.Controllers
{
    public class FilterController : ControllerBase
    {
        private readonly IFilterService _filterService;
        public FilterController(IFilterService filterService)
        {
            _filterService = filterService;
        }

        [HttpGet("GetFilters")]
        public async Task<IActionResult> GetFilters()
        {
            var result = await _filterService.GetFiltersAsync();
            return Ok(result);
        }
        [HttpGet("GetFilterById/{filterId}")]
        public async Task<IActionResult> GetFilterById(int filterId)
        {
            var result = await _filterService.GetFilterByIdAsync(filterId);
            return Ok(result);
        }

        [HttpPost("CreateFilter")]
        public async Task<IActionResult> CreateFilter([FromBody] FilterRequest request)
        {
            await _filterService.CreateFilterAsync(request);
            return Ok("Filter created successfully");
        }

        [HttpPut("UpdateFilter/{filterId}")]
        public async Task<IActionResult> UpdateFilter(int filterId, [FromBody] FilterRequest request)
        {
            await _filterService.UpdateFilterAsync(filterId, request);
            return Ok("Filter updated successfully");
        }

        [HttpDelete("DeleteFilter/{filterId}")]
        public async Task<IActionResult> DeleteFilter(int filterId)
        {
            await _filterService.DeleteFilterAsync(filterId);
            return Ok("Filter deleted successfully");
        }
    }
}
