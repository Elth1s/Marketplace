using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebAPI.Interfaces;
using WebAPI.ViewModels.Request.Countries;
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
        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
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
        [Authorize(Roles = "Admin")]
        [HttpGet("GetCountries")]
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
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(SearchCountryResponse))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpGet("SearchCountries")]
        public async Task<IActionResult> SearchCountries([FromQuery] SearchCountryRequest request)
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
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(CountryResponse))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpGet("GetCountryById/{countryId}")]
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
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>        
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpPost("CreateCountry")]
        public async Task<IActionResult> CreateCountry([FromBody] CountryRequest request)
        {
            await _countryService.CreateCountryAsync(request);
            return Ok("Country created successfully");
        }

        /// <summary>
        /// Update an existing country
        /// </summary>
        /// <param name="countryId">Country identifier</param>
        /// <param name="request">Country</param>
        /// <response code="200">Country update completed successfully</response>
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
        [HttpPut("UpdateCountry/{countryId}")]
        public async Task<IActionResult> UpdateCountry(int countryId, [FromBody] CountryRequest request)
        {
            await _countryService.UpdateCountryAsync(countryId, request);
            return Ok("Country updated successfully");
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
        [HttpDelete("DeleteCountry/{countryId}")]
        public async Task<IActionResult> DeleteCountry(int countryId)
        {
            await _countryService.DeleteCountryAsync(countryId);
            return Ok("Country deleted successfully");
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
        [HttpDelete("DeleteCountries")]
        public async Task<IActionResult> DeleteCountries([FromQuery] IEnumerable<int> ids)
        {
            await _countryService.DeleteCountriesAsync(ids);
            return Ok("Countries deleted successfully");
        }
    }
}
