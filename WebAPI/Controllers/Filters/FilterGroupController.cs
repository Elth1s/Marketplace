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
    /// The filter group controller class
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class FilterGroupController : ControllerBase
    {
        private readonly IFilterGroupService _filterGroupService;
        private readonly IStringLocalizer<FilterGroupController> _filterGroupLocalizer;

        public FilterGroupController(IFilterGroupService filterGroupService,
             IStringLocalizer<FilterGroupController> filterGroupLocalizer)
        {
            _filterGroupService = filterGroupService;
            _filterGroupLocalizer = filterGroupLocalizer;
        }

        /// <summary>
        /// Returns all filter groups
        /// </summary>
        /// <response code="200">Getting filter groups completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="500">An internal error has occurred</response> 
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<FilterGroupResponse>))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpGet("Get")]
        public async Task<IActionResult> GetFilterGroups()
        {
            var result = await _filterGroupService.GetFilterGroupsAsync();
            return Ok(result);
        }

        /// <summary>
        /// Return of sorted filter groups
        /// </summary>
        /// <response code="200">Getting filter groups completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(SearchResponse<FilterGroupResponse>))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpGet("Search")]
        public async Task<IActionResult> SearchFilterGroups([FromQuery] AdminSearchRequest request)
        {
            var result = await _filterGroupService.SearchAsync(request);
            return Ok(result);
        }

        /// <summary>
        /// Returns filter group with the given identifier
        /// </summary>
        /// <param name="filterGroupId">Filter group id</param>
        /// <response code="200">Getting filter group completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="404">Filter group not found</response>
        /// <response code="500">An internal error has occurred</response> 
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(FilterGroupFullInfoResponse))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpGet("GetById/{filterGroupId}")]
        public async Task<IActionResult> GetFilterGroupById(int filterGroupId)
        {
            var result = await _filterGroupService.GetFilterGroupByIdAsync(filterGroupId);
            return Ok(result);
        }

        /// <summary>
        /// Create new filter group
        /// </summary>
        /// <param name="request">New filter group</param>
        /// <response code="200">Filter group creation completed successfully</response>
        /// <response code="400">Filter group name not unique</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="500">An internal error has occurred</response> 
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpPost("Create")]
        public async Task<IActionResult> CreateFilterGroup([FromBody] FilterGroupRequest request)
        {
            await _filterGroupService.CreateFilterGroupAsync(request);
            return Ok(_filterGroupLocalizer["CreateSuccess"].Value);
        }

        /// <summary>
        /// Update an existing filter group
        /// </summary>
        /// <param name="filterGroupId">Filter group identifier</param>
        /// <param name="request">Filter group</param>
        /// <response code="200">Filter group update completed successfully</response>
        /// <response code="400">Filter group name not unique</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="404">Filter group not found</response>
        /// <response code="500">An internal error has occurred</response> 
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpPut("Update/{filterGroupId}")]
        public async Task<IActionResult> UpdateFilterGroup(int filterGroupId, [FromBody] FilterGroupRequest request)
        {
            await _filterGroupService.UpdateFilterGroupAsync(filterGroupId, request);
            return Ok(_filterGroupLocalizer["UpdateSuccess"].Value);
        }

        /// <summary>
        /// Delete an existing filter group
        /// </summary>
        /// <param name="filterGroupId">Filter group identifier</param>
        /// <response code="200">Filter group deletion completed successfully</response>
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
        [HttpDelete("Delete/{filterGroupId}")]
        public async Task<IActionResult> DeleteFilterGroup(int filterGroupId)
        {
            await _filterGroupService.DeleteFilterGroupAsync(filterGroupId);
            return Ok(_filterGroupLocalizer["DeleteSuccess"].Value);
        }

        /// <summary>
        /// Delete an existing filter groups
        /// </summary>
        /// <response code="200">Filter groups deletion completed successfully</response>
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
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] IEnumerable<int> ids)
        {
            await _filterGroupService.DeleteAsync(ids);
            return Ok(_filterGroupLocalizer["DeleteListSuccess"].Value);
        }
    }
}
