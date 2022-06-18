﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;
using WebAPI.Interfaces;
using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Response;

namespace WebAPI.Controllers
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

        private readonly IShopService _shopService;
        public ShopController(IShopService shopService)
        {
            _shopService = shopService;
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
        [HttpGet("GetShops")]
        public async Task<IActionResult> GetShops()
        {
            var result = await _shopService.GetShopsAsync();
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
        [HttpGet("GetShopById/{shopId}")]
        public async Task<IActionResult> GetShopById(int shopId)
        {
            var result = await _shopService.GetShopByIdAsync(shopId);
            return Ok(result);
        }

        /// <summary>
        /// Create new shop
        /// </summary>
        /// <param name="request">New shop</param>
        /// <response code="200">Shop creation completed successfully</response>
        /// <response code="400">User adding role failed</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="404">User not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpPost("CreateShop")]
        public async Task<IActionResult> CreateShop([FromBody] ShopRequest request)
        {
            await _shopService.CreateShopAsync(request, UserId);
            return Ok("Shop created successfully");
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
        [HttpPut("UpdateShop/{shopId}")]
        public async Task<IActionResult> UpdateShop(int shopId, [FromBody] ShopRequest request)
        {
            await _shopService.UpdateShopAsync(shopId, request, UserId);
            return Ok("Shop updated successfully");
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
        [HttpDelete("DeleteShop/{shopId}")]
        public async Task<IActionResult> DeleteShop(int shopId)
        {
            await _shopService.DeleteShopAsync(shopId);
            return Ok("Shop deleted successfully");
        }
    }
}
