using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebAPI.Interfaces.Filters;
using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Request.Filters;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Filters;

namespace WebAPI.Controllers.Filters
{
    /// <summary>
    /// The filter name controller class
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class FilterNameController : ControllerBase
    {
        private readonly IFilterNameService _filterNameService;
        public FilterNameController(IFilterNameService filterNameService)
        {
            _filterNameService = filterNameService;
        }

        /// <summary>
        /// Returns all filters name
        /// </summary>
        /// <response code="200">Getting filters name completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<FilterNameResponse>))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpGet("Get")]
        public async Task<IActionResult> GetFiltersName()
        {
            var result = await _filterNameService.GetFiltersNameAsync();
            return Ok(result);
        }

        /// <summary>
        /// Return of sorted filter names
        /// </summary>
        /// <response code="200">Getting filter names completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(AdminSearchResponse<FilterNameResponse>))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpGet("Search")]
        public async Task<IActionResult> SearchFilters([FromQuery] AdminSearchRequest request)
        {
            var result = await _filterNameService.SearchAsync(request);
            return Ok(result);
        }

        /// <summary>
        /// Returns filter name with the given identifier
        /// </summary>
        /// <param name="filterNameId">Filter name identifier</param>
        /// <response code="200">Getting filter name completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="404">Filter name not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(FilterNameResponse))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpGet("GetById/{filterNameId}")]
        public async Task<IActionResult> GetFilterNameById(int filterNameId)
        {
            var result = await _filterNameService.GetFilterNameByIdAsync(filterNameId);
            return Ok(result);
        }

        /// <summary>
        /// Create new filter name
        /// </summary>
        /// <param name="request">New filter name</param>
        /// <response code="200">Filter name creation completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="404">Filter group not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpPost("Create")]
        public async Task<IActionResult> CreateFilterName([FromBody] FilterNameRequest request)
        {
            await _filterNameService.CreateFilterNameAsync(request);
            return Ok("Filter name created successfully");
        }

        /// <summary>
        /// Update an existing filter name
        /// </summary>
        /// <param name="filterNameId">Filter name identifier</param>
        /// <param name="request">Filter name</param>
        /// <response code="200">Filter name update completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="404">Filter group or filter name not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpPut("Update/{filterNameId}")]
        public async Task<IActionResult> UpdateFilterName(int filterNameId, [FromBody] FilterNameRequest request)
        {
            await _filterNameService.UpdateFilterNameAsync(filterNameId, request);
            return Ok("Filter name updated successfully");
        }

        /// <summary>
        /// Delete an existing filter name
        /// </summary>
        /// <param name="filterNameId">Filter name identifier</param>
        /// <response code="200">Filter name deletion completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="404">Filter name not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete/{filterNameId}")]
        public async Task<IActionResult> DeleteFilterName(int filterNameId)
        {
            await _filterNameService.DeleteFilterNameAsync(filterNameId);
            return Ok("Filter name deleted successfully");
        }

        /// <summary>
        /// Delete an existing filter names
        /// </summary>
        /// <response code="200">Filter name deletion completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="404">Filter name not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] IEnumerable<int> ids)
        {
            await _filterNameService.DeleteAsync(ids);
            return Ok("Filter names deleted successfully");
        }
    }
}
