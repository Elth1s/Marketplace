using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Interfaces;
using WebAPI.ViewModels.Request;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;
        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetCountries")]
        public async Task<IActionResult> GetCountries()
        {
            var result = await _countryService.GetCountriesAsync();
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetCountryById/{countryId}")]
        public async Task<IActionResult> GetCountryById(int countryId)
        {
            var result = await _countryService.GetCountryByIdAsync(countryId);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("CreateCountry")]
        public async Task<IActionResult> CreateCountry([FromBody] CountryRequest request)
        {
            await _countryService.CreateCountryAsync(request);
            return Ok("Country created successfully");
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateCountry/{countryId}")]
        public async Task<IActionResult> UpdateCountry(int countryId, [FromBody] CountryRequest request)
        {
            await _countryService.UpdateCountryAsync(countryId, request);
            return Ok("Country updated successfully");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteCountry/{countryId}")]
        public async Task<IActionResult> DeleteCountry(int countryId)
        {
            await _countryService.DeleteCountryAsync(countryId);
            return Ok("Country deleted successfully");
        }
    }
}
