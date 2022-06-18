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
    public class CharacteristicController : Controller
    {
        private readonly ICharacteristicService _characteristicService;

        public CharacteristicController(ICharacteristicService characteristicService)
        {
            _characteristicService = characteristicService;
        }

        /// <summary>
        /// Returns all characteristics
        /// </summary>
        /// <response code="200">Getting characteristics completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="500">An internal error has occurred</response> 
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<CharacteristicResponse>))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin,Seller")]
        [HttpGet("Get")]
        public async Task<IActionResult> Get()
        {
            var result = await _characteristicService.GetAsync();
            return Ok(result);
        }

        /// <summary>
        /// Returns characteristic with the given identifier
        /// </summary>
        /// <param name="id">Characteristic identifier</param>
        /// <response code="200">Getting characteristic completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="404">Characteristic not found</response>
        /// <response code="500">An internal error has occurred</response> 
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(CharacteristicResponse))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin,Seller")]
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _characteristicService.GetByIdAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// Create new characteristic
        /// </summary>
        /// <param name="request">New characteristic</param>
        /// <response code="200">Characteristic creation completed successfully</response>
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
        public async Task<IActionResult> Create([FromBody] CharacteristicRequest request)
        {
            await _characteristicService.CreateAsync(request);
            return Ok("Characteristic updated successfully");
        }

        /// <summary>
        /// Update an existing characteristic
        /// </summary>
        /// <param name="id">Characteristic identifier</param>
        /// <param name="request">Characteristic</param>
        /// <response code="200">Characteristic update completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="404">Characteristic group or characteristic not found</response>
        /// <response code="500">An internal error has occurred</response> 
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin,Seller")]
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CharacteristicRequest request)
        {
            await _characteristicService.UpdateAsync(id, request);
            return Ok("Characteristic updated successfully");
        }

        /// <summary>
        /// Delete an existing characteristic
        /// </summary>
        /// <param name="id">Characteristic identifier</param>
        /// <response code="200">Characteristic deletion completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="404">Characteristic not found</response>
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
            await _characteristicService.DeleteAsync(id);
            return Ok("Characteristic deleted successfully");
        }
    }
}
