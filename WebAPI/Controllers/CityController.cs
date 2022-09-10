using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;
using WebAPI.Interfaces;
using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Cities;

namespace WebAPI.Controllers
{
    /// <summary>
    /// The city controller class
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;
        private readonly IStringLocalizer<CityController> _cityLocalizer;

        public CityController(ICityService cityService, IStringLocalizer<CityController> cityLocalizer)
        {
            _cityService = cityService;
            _cityLocalizer = cityLocalizer;
        }

        /// <summary>
        /// Returns all cities
        /// </summary>
        /// <response code="200">Getting cities completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<CityResponse>))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpGet("Get")]
        public async Task<IActionResult> GetCities()
        {
            var result = await _cityService.GetCitiesAsync();
            return Ok(result);
        }

        /// <summary>
        /// Returns cities
        /// </summary>
        /// <response code="200">Getting cities completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<CityForSelectResponse>))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpGet("GetByCountry/{countryId}")]
        public async Task<IActionResult> GetCitiesByCountry(int countryId)
        {
            var result = await _cityService.GetCitiesByCountryAsync(countryId);
            return Ok(result);
        }

        /// <summary>
        /// Return of sorted cities
        /// </summary>
        /// <response code="200">Getting cities completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(SearchResponse<CityResponse>))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpGet("Search")]
        public async Task<IActionResult> SearchCities([FromQuery] AdminSearchRequest request)
        {
            var result = await _cityService.SearchCitiesAsync(request);
            return Ok(result);
        }

        /// <summary>
        /// Returns city with the given identifier
        /// </summary>
        /// <param name="cityId">City identifier</param>
        /// <response code="200">Getting city completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="404">City not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(CityFullInfoResponse))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpGet("GetById/{cityId}")]
        public async Task<IActionResult> GetCityById(int cityId)
        {
            var result = await _cityService.GetCityByIdAsync(cityId);
            return Ok(result);
        }

        /// <summary>
        /// Create new city
        /// </summary>
        /// <param name="request">New city</param>
        /// <response code="200">City creation completed successfully</response>
        /// <response code="400">City name not unique</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="404">Country not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpPost("Create")]
        public async Task<IActionResult> CreateCity([FromBody] CityRequest request)
        {
            await _cityService.CreateCityAsync(request);
            return Ok(_cityLocalizer["CreateSuccess"].Value);
        }

        /// <summary>
        /// Update an existing city
        /// </summary>
        /// <param name="cityId">City identifier</param>
        /// <param name="request">City</param>
        /// <response code="200">City update completed successfully</response>
        /// <response code="400">City name not unique</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="404">Country or city not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpPut("Update/{cityId}")]
        public async Task<IActionResult> UpdateCity(int cityId, [FromBody] CityRequest request)
        {
            await _cityService.UpdateCityAsync(cityId, request);
            return Ok(_cityLocalizer["UpdateSuccess"].Value);
        }

        /// <summary>
        /// Delete an existing city
        /// </summary>
        /// <param name="cityId">City identifier</param>
        /// <response code="200">City deletion completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="404">City not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete/{cityId}")]
        public async Task<IActionResult> DeleteCity(int cityId)
        {
            await _cityService.DeleteCityAsync(cityId);
            return Ok(_cityLocalizer["DeleteSuccess"].Value);
        }

        /// <summary>
        /// Delete an existing cities
        /// </summary>
        /// <response code="200">Cities deletion completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="404">City not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteCities([FromQuery] IEnumerable<int> ids)
        {
            await _cityService.DeleteCitiesAsync(ids);
            return Ok(_cityLocalizer["DeleteListSuccess"].Value);
        }
    }
}
