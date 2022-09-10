using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebAPI.Interfaces.Users;
using WebAPI.ViewModels.Request.Users;

namespace WebAPI.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenderController : ControllerBase
    {
        private readonly IGenderService _genderService;

        public GenderController(IGenderService genderService)
        {
            _genderService = genderService;
        }

        /// <summary>
        /// Returns all genders
        /// </summary>
        /// <response code="200">Getting genders completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<GenderResponse>))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpGet("Get")]
        public async Task<IActionResult> GetGenders()
        {
            var result = await _genderService.GetGendersAsync();
            return Ok(result);
        }
    }
}
