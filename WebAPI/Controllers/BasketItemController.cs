using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;
using WebAPI.Interfaces;
using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Response;

namespace WebAPI.Controllers
{
    /// <summary>
    /// The basket item controller class.
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class BasketItemController : ControllerBase
    {
        private string UserId => User?.FindFirstValue(ClaimTypes.NameIdentifier);
        private readonly IBasketItemService _basketItemService;

        public BasketItemController(IBasketItemService basketItemService)
        {
            _basketItemService = basketItemService;
        }

        /// <summary>
        /// Returns baskets with the user identifier
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <response code="200">Getting basket completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="404">Basket not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(BasketResponse))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpGet("GetAll/{userId}")]
        public async Task<IActionResult> GetAll(string userId)
        {
            var result = await _basketItemService.GetAllAsync(userId);
            return Ok(result);
        }

        /// <summary>
        /// Create new basket item
        /// </summary>
        /// <param name="request">New basket</param>
        /// <response code="200">Basket creation completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="404">Basket not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] BasketCreateRequest request)
        {
            await _basketItemService.CreateAsync(request, UserId);
            return Ok("Product created successfully");
        }



        /// <summary>
        /// Delete an existing basket
        /// </summary>
        /// <param name="basketId">Basket identifier</param>
        /// <response code="200">Basket update completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="404">Basket not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpDelete("DeleteBasket/{basketId}")]
        public async Task<IActionResult> DeleteBasket(int basketId)
        {
            await _basketItemService.DeleteAsync(basketId);
            return Ok("Basket deleted successfully");
        }


        /// <summary>
        /// Update an existing basket
        /// </summary>
        /// <param name="basketId">Basket identifier</param>
        /// <param name="request">Basket</param>
        /// <response code="200">Basket update completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="404">Basket not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpPut("UpdateBasket/{basketId}")]
        public async Task<IActionResult> UpdateCity(int basketId, [FromBody] BasketUpdateRequest request)
        {
            await _basketItemService.UpdateAsync(basketId, request, UserId);
            return Ok("Basket updated successfully");
        }


    }
}
