using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;
using WebAPI.Helpers;
using WebAPI.Interfaces.Shops;
using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Request.Shops;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Shops;
using WebAPI.ViewModels.Response.Users;

namespace WebAPI.Controllers.Shops
{
    /// <summary>
    /// The shop controller class
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private string UserId => User?.FindFirstValue(ClaimTypes.NameIdentifier);

        private readonly IStringLocalizer<ShopController> _shopLocalizer;
        private readonly IShopService _shopService;
        public ShopController(IShopService shopService, IStringLocalizer<ShopController> shopLocalizer)
        {
            _shopService = shopService;
            _shopLocalizer = shopLocalizer;
        }

        /// <summary>
        /// Returns all shops
        /// </summary>
        /// <response code="200">Getting shops completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<ShopResponse>))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpGet("Get")]
        public async Task<IActionResult> GetShops()
        {
            var result = await _shopService.GetShopsAsync();
            return Ok(result);
        }

        /// <summary>
        /// Return of sorted shops
        /// </summary>
        /// <response code="200">Getting shops completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(SearchResponse<ShopResponse>))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin,Seller")]
        [HttpGet("Search")]
        public async Task<IActionResult> SearchShops([FromQuery] AdminSearchRequest request)
        {
            var result = await _shopService.SearchShopsAsync(request);
            return Ok(result);
        }

        /// <summary>
        /// Return of shop info form product
        /// </summary>
        /// /// <param name="shopId">Shop identifier</param>
        /// <response code="200">Getting shops completed successfully</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ShopInfoFromProductResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [HttpGet("ShopInfoFromProduct/{shopId}")]
        public async Task<IActionResult> ShopInfoFromProduct(int shopId)
        {
            var result = await _shopService.ShopInfoFromProductAsync(shopId);
            return Ok(result);
        }

        /// <summary>
        /// Return shop info
        /// </summary>
        /// /// <param name="shopId">Shop identifier</param>
        /// <response code="200">Getting shop info completed successfully</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ShopPageInfoResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [HttpGet("GetShopInfo/{shopId}")]
        public async Task<IActionResult> GetShopInfo(int shopId)
        {
            var result = await _shopService.GetShopInfoAsync(shopId);
            return Ok(result);
        }


        /// <summary>
        /// Returns shop with the given identifier
        /// </summary>
        /// <param name="shopId">Shop identifier</param>
        /// <response code="200">Getting shop completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="404">Shop not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ShopResponse))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin,Seller")]
        [HttpGet("GetById/{shopId}")]
        public async Task<IActionResult> GetShopById(int shopId)
        {
            var result = await _shopService.GetShopByIdAsync(shopId);
            return Ok(result);
        }

        /// <summary>
        /// Create new shop
        /// </summary>
        /// <param name="request">New shop</param>
        /// <response code="201">Shop creation completed successfully</response>
        /// <response code="400">User adding role failed</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="404">User not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status201Created, Type = typeof(AuthResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpPost("Create")]
        public async Task<IActionResult> CreateShop([FromBody] ShopRequest request)
        {
            var response = await _shopService.CreateShopAsync(request, UserId, IpUtil.GetIpAddress(Request, HttpContext));
            //return Created(_shopLocalizer["CreateSuccess"].Value, response);
            return Created("", response);
        }

        /// <summary>
        /// Update an existing shop
        /// </summary>
        /// <param name="shopId">Shop identifier</param>
        /// <param name="request">Shop</param>
        /// <response code="200">Shop update completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="404">User or shop not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin,Seller")]
        [HttpPut("Update/{shopId}")]
        public async Task<IActionResult> UpdateShop(int shopId, [FromBody] ShopRequest request)
        {
            await _shopService.UpdateShopAsync(shopId, request, UserId);
            return Ok(_shopLocalizer["UpdateSuccess"].Value);
        }

        /// <summary>
        /// Delete an existing shop
        /// </summary>
        /// <param name="shopId">Shop identifier</param>
        /// <response code="200">Shop update completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="404">Shop not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin,Seller")]
        [HttpDelete("Delete/{shopId}")]
        public async Task<IActionResult> DeleteShop(int shopId)
        {
            await _shopService.DeleteShopAsync(shopId);
            return Ok(_shopLocalizer["DeleteSuccess"].Value);
        }

        /// <summary>
        /// Delete an existing shops
        /// </summary>
        /// <response code="200">Shops deletion completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="404">Shop not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteShops([FromQuery] IEnumerable<int> ids)
        {
            await _shopService.DeleteShopsAsync(ids);
            return Ok(_shopLocalizer["DeleteListSuccess"].Value);
        }
    }
}
