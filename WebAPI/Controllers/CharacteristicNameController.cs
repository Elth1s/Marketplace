using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Response;

namespace WebAPI.Interfaces
{
    /// <summary>
    /// The characteristic controller class.
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class CharacteristicNameController : Controller
    {
        private readonly ICharacteristicNameService _characteristicNameService;

        public CharacteristicNameController(ICharacteristicNameService characteristicService)
        {
            _characteristicNameService = characteristicService;
        }

        /// <summary>
        /// Returns all characteristic names
        /// </summary>
        /// <response code="200">Getting characteristic names completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="500">An internal error has occurred</response> 
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<CharacteristicNameResponse>))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin,Seller")]
        [HttpGet("Get")]
        public async Task<IActionResult> Get()
        {
            var result = await _characteristicNameService.GetAsync();
            return Ok(result);
        }

        /// <summary>
        /// Returns characteristic name with the given identifier
        /// </summary>
        /// <param name="id">Characteristic name identifier</param>
        /// <response code="200">Getting characteristic name completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="404">Characteristic name not found</response>
        /// <response code="500">An internal error has occurred</response> 
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(CharacteristicNameResponse))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin,Seller")]
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _characteristicNameService.GetByIdAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// Create new characteristic name
        /// </summary>
        /// <param name="request">New characteristic name</param>
        /// <response code="200">Characteristic name creation completed successfully</response>
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
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CharacteristicNameRequest request)
        {
            await _characteristicNameService.CreateAsync(request);
            return Ok("Characteristic name updated successfully");
        }

        /// <summary>
        /// Update an existing characteristic name
        /// </summary>
        /// <param name="id">Characteristic name identifier</param>
        /// <param name="request">Characteristic name</param>
        /// <response code="200">Characteristic name update completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="404">Characteristic group or characteristic name not found</response>
        /// <response code="500">An internal error has occurred</response> 
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin,Seller")]
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CharacteristicNameRequest request)
        {
            await _characteristicNameService.UpdateAsync(id, request);
            return Ok("Characteristic name updated successfully");
        }

        /// <summary>
        /// Delete an existing characteristic name
        /// </summary>
        /// <param name="id">Characteristic name identifier</param>
        /// <response code="200">Characteristic name deletion completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="404">Characteristic name not found</response>
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
            await _characteristicNameService.DeleteAsync(id);
            return Ok("Characteristic name deleted successfully");
        }
    }
}
