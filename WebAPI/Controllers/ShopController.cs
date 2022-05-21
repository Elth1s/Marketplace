using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebAPI.Interfaces;
using WebAPI.ViewModels.Request;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private string UserId => User?.FindFirstValue(ClaimTypes.NameIdentifier);

        private readonly IShopService _shopService;
        public ShopController(IShopService shopService)
        {
            _shopService = shopService;
        }

        [HttpGet("GetShops")]
        public async Task<IActionResult> GetShops()
        {
            var result = await _shopService.GetShopsAsync();
            return Ok(result);
        }
        [HttpGet("GetShopById/{shopId}")]
        public async Task<IActionResult> GetShopById(int shopId)
        {
            var result = await _shopService.GetShopByIdAsync(shopId);
            return Ok(result);
        }

        [HttpPost("CreateShop")]
        public async Task<IActionResult> CreateShop([FromBody] ShopRequest request)
        {
            await _shopService.CreateShopAsync(request, UserId);
            return Ok("Shop created successfully");
        }

        [HttpPut("UpdateShop/{shopId}")]
        public async Task<IActionResult> UpdateShop(int shopId, [FromBody] ShopRequest request)
        {
            await _shopService.UpdateShopAsync(shopId, request, UserId);
            return Ok("Shop updated successfully");
        }

        [HttpDelete("DeleteShop/{shopId}")]
        public async Task<IActionResult> DeleteShop(int shopId)
        {
            await _shopService.DeleteShopAsync(shopId);
            return Ok("Shop deleted successfully");
        }
    }
}
