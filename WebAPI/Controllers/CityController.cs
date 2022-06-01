using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Interfaces;
using WebAPI.ViewModels.Request;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;
        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetCities")]
        public async Task<IActionResult> GetCities()
        {
            var result = await _cityService.GetCitiesAsync();
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetCityById/{cityId}")]
        public async Task<IActionResult> GetCityById(int cityId)
        {
            var result = await _cityService.GetCityByIdAsync(cityId);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("CreateCity")]
        public async Task<IActionResult> CreateCity([FromBody] CityRequest request)
        {
            await _cityService.CreateCityAsync(request);
            return Ok("City created successfully");
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateCity/{cityId}")]
        public async Task<IActionResult> UpdateCity(int cityId, [FromBody] CityRequest request)
        {
            await _cityService.UpdateCityAsync(cityId, request);
            return Ok("City updated successfully");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteCity/{cityId}")]
        public async Task<IActionResult> DeleteCity(int cityId)
        {
            await _cityService.DeleteCityAsync(cityId);
            return Ok("City deleted successfully");
        }
    }
}
