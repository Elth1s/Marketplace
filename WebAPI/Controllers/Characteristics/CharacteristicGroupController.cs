using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;
using WebAPI.Interfaces.Characteristics;
using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Request.Characteristics;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Characteristics;

namespace WebAPI.Controllers.Characteristics
{
    /// <summary>
    /// The characteristic group controller class
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class CharacteristicGroupController : ControllerBase
    {
        private string UserId => User?.FindFirstValue(ClaimTypes.NameIdentifier);

        private readonly IStringLocalizer<CharacteristicGroupController> _characteristicGroupLocalizer;
        private readonly ICharacteristicGroupService _characteristicGroupService;
        public CharacteristicGroupController(ICharacteristicGroupService characteristicGroupService,
            IStringLocalizer<CharacteristicGroupController> characteristicGroupLocalizer)
        {
            _characteristicGroupService = characteristicGroupService;
            _characteristicGroupLocalizer = characteristicGroupLocalizer;
        }

        /// <summary>
        /// Returns all characteristic groups
        /// </summary>
        /// <response code="200">Getting characteristic groups completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="404">User not found</response>
        /// <response code="500">An internal error has occurred</response> 
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<CharacteristicGroupResponse>))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin,Seller")]
        [HttpGet("Get")]
        public async Task<IActionResult> Get()
        {
            var result = await _characteristicGroupService.GetAsync(UserId);
            return Ok(result);
        }



        /// <summary>
        /// Return of sorted characteristic groups
        /// </summary>
        /// <response code="200">Getting characteristic groups completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(SearchResponse<CharacteristicGroupResponse>))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin,Seller")]
        [HttpGet("Search")]
        public async Task<IActionResult> SearchCharacteristicGroups([FromQuery] SellerSearchRequest request)
        {
            var result = await _characteristicGroupService.SearchCharacteristicGroupsAsync(request, UserId);
            return Ok(result);
        }

        /// <summary>
        /// Returns characteristic group with the given identifier
        /// </summary>
        /// <param name="id">Characteristic group id</param>
        /// <response code="200">Getting characteristic group completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="404">Characteristic group not found</response>
        /// <response code="500">An internal error has occurred</response> 
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(CharacteristicGroupResponse))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin,Seller")]
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _characteristicGroupService.GetByIdAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// Create new characteristic group
        /// </summary>
        /// <param name="request">New characteristic group</param>
        /// <response code="200">Characteristic group creation completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="404">User not found</response>
        /// <response code="500">An internal error has occurred</response> 
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin,Seller")]
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CharacteristicGroupRequest request)
        {
            await _characteristicGroupService.CreateAsync(request, UserId);
            return Ok(_characteristicGroupLocalizer["CreateSuccess"].Value);
        }

        /// <summary>
        /// Update an existing characteristic group
        /// </summary>
        /// <param name="id">Characteristic group identifier</param>
        /// <param name="request">Characteristic group</param>
        /// <response code="200">Characteristic group update completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="404">Characteristic group not found</response>
        /// <response code="500">An internal error has occurred</response> 
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin,Seller")]
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CharacteristicGroupRequest request)
        {
            await _characteristicGroupService.UpdateAsync(id, request, UserId);
            return Ok(_characteristicGroupLocalizer["UpdateSuccess"].Value);
        }

        /// <summary>
        /// Delete an existing characteristic group
        /// </summary>
        /// <param name="id">Characteristic group identifier</param>
        /// <response code="200">Characteristic group deletion completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="404">Characteristic group not found</response>
        /// <response code="500">An internal error has occurred</response> 
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin,Seller")]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _characteristicGroupService.DeleteAsync(id);
            return Ok(_characteristicGroupLocalizer["DeleteSuccess"].Value);
        }

        /// <summary>
        /// Delete an existing Characteristic groups
        /// </summary>
        /// <response code="200">Characteristic groups deletion completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="404">Characteristic group not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin,Seller")]
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] IEnumerable<int> ids)
        {
            await _characteristicGroupService.DeleteAsync(ids);
            return Ok(_characteristicGroupLocalizer["DeleteListSuccess"].Value);
        }
    }
}
