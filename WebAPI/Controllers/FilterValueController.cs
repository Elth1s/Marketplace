﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebAPI.Interfaces;
using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Response;

namespace WebAPI.Controllers
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
        public FilterValueController(IFilterValueService filterValueService)
        {
            _filterValueService = filterValueService;
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
        [HttpGet("GetFiltersValue")]
        public async Task<IActionResult> GetFiltersValue()
        {
            var result = await _filterValueService.GetFiltersValueAsync();
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
        [HttpGet("GetFilterValueById/{filterValueId}")]
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
        [HttpPost("CreateFilterValue")]
        public async Task<IActionResult> CreateFilterValue([FromBody] FilterValueRequest request)
        {
            await _filterValueService.CreateFilterValueAsync(request);
            return Ok("Filter value created successfully");
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
        [HttpPut("UpdateFilterValue/{filterValueId}")]
        public async Task<IActionResult> UpdateFilterValue(int filterValueId, [FromBody] FilterValueRequest request)
        {
            await _filterValueService.UpdateFilterValueAsync(filterValueId, request);
            return Ok("Filter value updated successfully");
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
        [HttpDelete("DeleteFilterValue/{filterValueId}")]
        public async Task<IActionResult> DeleteFilterValue(int filterValueId)
        {
            await _filterValueService.DeleteFilterValueAsync(filterValueId);
            return Ok("Filter value deleted successfully");
        }
    }
}
