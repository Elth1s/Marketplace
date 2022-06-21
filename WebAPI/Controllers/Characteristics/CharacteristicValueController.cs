using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebAPI.Interfaces.Characteristics;
using WebAPI.ViewModels.Request.Characteristics;
using WebAPI.ViewModels.Response.Characteristics;

namespace WebAPI.Controllers.Characteristics
{
    /// <summary>
    /// The characteristic controller class.
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class CharacteristicValueController : Controller
    {
        private readonly ICharacteristicValueService _characteristicValueService;

        public CharacteristicValueController(ICharacteristicValueService characteristicValueService)
        {
            _characteristicValueService = characteristicValueService;
        }

        /// <summary>
        /// Returns all characteristic values
        /// </summary>
        /// <response code="200">Getting characteristic values completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="500">An internal error has occurred</response> 
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<CharacteristicValueResponse>))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin,Seller")]
        [HttpGet("Get")]
        public async Task<IActionResult> Get()
        {
            var result = await _characteristicValueService.GetAsync();
            return Ok(result);
        }

        /// <summary>
        /// Returns characteristic value with the given identifier
        /// </summary>
        /// <param name="id">Characteristic value identifier</param>
        /// <response code="200">Getting characteristic value completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="404">Characteristic value not found</response>
        /// <response code="500">An internal error has occurred</response> 
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(CharacteristicValueResponse))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin,Seller")]
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _characteristicValueService.GetByIdAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// Create new characteristic value
        /// </summary>
        /// <param name="request">New characteristic value</param>
        /// <response code="200">Characteristic value creation completed successfully</response>
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
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CharacteristicValueRequest request)
        {
            await _characteristicValueService.CreateAsync(request);
            return Ok("Characteristic value updated successfully");
        }

        /// <summary>
        /// Update an existing characteristic value
        /// </summary>
        /// <param name="id">Characteristic value identifier</param>
        /// <param name="request">Characteristic value</param>
        /// <response code="200">Characteristic value update completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="404">Characteristic name or characteristic value not found</response>
        /// <response code="500">An internal error has occurred</response> 
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin,Seller")]
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CharacteristicValueRequest request)
        {
            await _characteristicValueService.UpdateAsync(id, request);
            return Ok("Characteristic value updated successfully");
        }

        /// <summary>
        /// Delete an existing characteristic value
        /// </summary>
        /// <param name="id">Characteristic value identifier</param>
        /// <response code="200">Characteristic value deletion completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="404">Characteristic value not found</response>
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
            await _characteristicValueService.DeleteAsync(id);
            return Ok("Characteristic value deleted successfully");
        }
    }
}
