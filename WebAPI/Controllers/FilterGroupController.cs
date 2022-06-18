using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebAPI.Interfaces;
using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Response;

namespace WebAPI.Controllers
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
        public FilterGroupController(IFilterGroupService filterGroupService)
        {
            _filterGroupService = filterGroupService;
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
        [HttpGet("GetFilterGroups")]
        public async Task<IActionResult> GetFilterGroups()
        {
            var result = await _filterGroupService.GetFilterGroupsAsync();
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
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(FilterGroupResponse))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpGet("GetFilterGroupById/{filterGroupId}")]
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
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="500">An internal error has occurred</response> 
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpPost("CreateFilterGroup")]
        public async Task<IActionResult> CreateFilterGroup([FromBody] FilterGroupRequest request)
        {
            await _filterGroupService.CreateFilterGroupAsync(request);
            return Ok("Filter group created successfully");
        }

        /// <summary>
        /// Update an existing filter group
        /// </summary>
        /// <param name="filterGroupId">Filter group identifier</param>
        /// <param name="request">Filter group</param>
        /// <response code="200">Filter group update completed successfully</response>
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
        [HttpPut("UpdateFilterGroup/{filterGroupId}")]
        public async Task<IActionResult> UpdateFilterGroup(int filterGroupId, [FromBody] FilterGroupRequest request)
        {
            await _filterGroupService.UpdateFilterGroupAsync(filterGroupId, request);
            return Ok("Filter group updated successfully");
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
        [HttpDelete("DeleteFilterGroup/{filterGroupId}")]
        public async Task<IActionResult> DeleteFilterGroup(int filterGroupId)
        {
            await _filterGroupService.DeleteFilterGroupAsync(filterGroupId);
            return Ok("Filter group deleted successfully");
        }
    }
}
