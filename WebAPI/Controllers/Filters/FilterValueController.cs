using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;
using WebAPI.Interfaces.Filters;
using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Request.Filters;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Filters;

namespace WebAPI.Controllers.Filters
{
    /// <summary>
    /// The filter value controller class
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class FilterValueController : ControllerBase
    {
        private readonly IFilterValueService _filterValueService;
        private readonly IStringLocalizer<FilterValueController> _filterValueLocalizer;

        public FilterValueController(IFilterValueService filterValueService,
            IStringLocalizer<FilterValueController> filterValueLocalizer)
        {
            _filterValueService = filterValueService;
            _filterValueLocalizer = filterValueLocalizer;
        }

        /// <summary>
        /// Returns all filters value
        /// </summary>
        /// <response code="200">Getting filters value completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<FilterValueResponse>))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpGet("Get")]
        public async Task<IActionResult> GetFiltersValue()
        {
            var result = await _filterValueService.GetFiltersValueAsync();
            return Ok(result);
        }

        /// <summary>
        /// Return of sorted filter values
        /// </summary>
        /// <response code="200">Getting filter values completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(AdminSearchResponse<FilterValueResponse>))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpGet("Search")]
        public async Task<IActionResult> SearchFilterValue([FromQuery] AdminSearchRequest request)
        {
            var result = await _filterValueService.SearchAsync(request);
            return Ok(result);
        }

        /// <summary>
        /// Returns filter value with the given identifier
        /// </summary>
        /// <param name="filterValueId">Filter value identifier</param>
        /// <response code="200">Getting filter value completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="404">Filter value not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(FilterNameResponse))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpGet("GetById/{filterValueId}")]
        public async Task<IActionResult> GetFilterValueById(int filterValueId)
        {
            var result = await _filterValueService.GetFilterValueByIdAsync(filterValueId);
            return Ok(result);
        }

        /// <summary>
        /// Create new filter value
        /// </summary>
        /// <param name="request">New filter value</param>
        /// <response code="200">Filter value creation completed successfully</response>
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
        [HttpPost("Create")]
        public async Task<IActionResult> CreateFilterValue([FromBody] FilterValueRequest request)
        {
            await _filterValueService.CreateFilterValueAsync(request);
            return Ok(_filterValueLocalizer["CreateSuccess"].Value);
        }

        /// <summary>
        /// Update an existing filter value
        /// </summary>
        /// <param name="filterValueId">Filter value identifier</param>
        /// <param name="request">Filter value</param>
        /// <response code="200">Filter value update completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="404">Filter name or filter value not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpPut("Update/{filterValueId}")]
        public async Task<IActionResult> UpdateFilterValue(int filterValueId, [FromBody] FilterValueRequest request)
        {
            await _filterValueService.UpdateFilterValueAsync(filterValueId, request);
            return Ok(_filterValueLocalizer["UpdateSuccess"].Value);
        }

        /// <summary>
        /// Delete an existing filter value
        /// </summary>
        /// <param name="filterValueId">Filter value identifier</param>
        /// <response code="200">Filter value deletion completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="404">Filter value not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete/{filterValueId}")]
        public async Task<IActionResult> DeleteFilterValue(int filterValueId)
        {
            await _filterValueService.DeleteFilterValueAsync(filterValueId);
            return Ok(_filterValueLocalizer["DeleteSuccess"].Value);
        }

        /// <summary>
        /// Delete an existing filter values
        /// </summary>
        /// <response code="200">Filter values deletion completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="404">Filter value not found</response>
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
            await _filterValueService.DeleteAsync(ids);
            return Ok(_filterValueLocalizer["DeleteListSuccess"].Value);
        }
    }
}
