using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;
using WebAPI.Interfaces;
using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Countries;

namespace WebAPI.Controllers
{
    /// <summary>
    /// The country controller class
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;
        private readonly IStringLocalizer<CountryController> _countryLocalizer;

        public CountryController(ICountryService countryService, IStringLocalizer<CountryController> countryLocalizer)
        {
            _countryService = countryService;
            _countryLocalizer = countryLocalizer;
        }

        /// <summary>
        /// Returns all countries
        /// </summary>
        /// <response code="200">Getting countries completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<CountryResponse>))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpGet("Get")]
        public async Task<IActionResult> GetCountries()
        {
            var result = await _countryService.GetCountriesAsync();
            return Ok(result);
        }

        /// <summary>
        /// Return of sorted countries
        /// </summary>
        /// <response code="200">Getting countries completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(SearchResponse<CountryResponse>))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpGet("Search")]
        public async Task<IActionResult> SearchCountries([FromQuery] AdminSearchRequest request)
        {
            var result = await _countryService.SearchCountriesAsync(request);
            return Ok(result);
        }

        /// <summary>
        /// Returns country with the given identifier
        /// </summary>
        /// <param name="countryId">Country identifier</param>
        /// <response code="200">Getting country completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="404">Country not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(CountryFullInfoResponse))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpGet("GetById/{countryId}")]
        public async Task<IActionResult> GetCountryById(int countryId)
        {
            var result = await _countryService.GetCountryByIdAsync(countryId);
            return Ok(result);
        }

        /// <summary>
        /// Create new country
        /// </summary>
        /// <param name="request">New country</param>
        /// <response code="200">Country creation completed successfully</response>
        /// <response code="400">Country name not unique</response>
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
        public async Task<IActionResult> CreateCountry([FromBody] CountryRequest request)
        {
            await _countryService.CreateCountryAsync(request);
            return Ok(_countryLocalizer["CreateSuccess"].Value);
        }

        /// <summary>
        /// Update an existing country
        /// </summary>
        /// <param name="countryId">Country identifier</param>
        /// <param name="request">Country</param>
        /// <response code="200">Country update completed successfully</response>
        /// <response code="400">Country name not unique</response>
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
        [HttpPut("Update/{countryId}")]
        public async Task<IActionResult> UpdateCountry(int countryId, [FromBody] CountryRequest request)
        {
            await _countryService.UpdateCountryAsync(countryId, request);
            return Ok(_countryLocalizer["UpdateSuccess"].Value);
        }

        /// <summary>
        /// Delete an existing country
        /// </summary>
        /// <response code="200">Country deletion completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="404">Country not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete/{countryId}")]
        public async Task<IActionResult> DeleteCountry(int countryId)
        {
            await _countryService.DeleteCountryAsync(countryId);
            return Ok(_countryLocalizer["DeleteSuccess"].Value);
        }

        /// <summary>
        /// Delete an existing countries
        /// </summary>
        /// <response code="200">Countries deletion completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="404">Country not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteCountries([FromQuery] IEnumerable<int> ids)
        {
            await _countryService.DeleteCountriesAsync(ids);
            return Ok(_countryLocalizer["DeleteListSuccess"].Value);
        }
    }
}
