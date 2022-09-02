using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;
using WebAPI.Interfaces;
using WebAPI.ViewModels.Request.Baskets;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Orders;

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

        private readonly IStringLocalizer<BasketItemController> _basketItemLocalizer;

        private readonly IBasketItemService _basketItemService;

        public BasketItemController(IBasketItemService basketItemService, IStringLocalizer<BasketItemController> basketItemLocalizer)
        {
            _basketItemService = basketItemService;
            _basketItemLocalizer = basketItemLocalizer;
        }

        /// <summary>
        /// Returns baskets by user identifier
        /// </summary>
        /// <response code="200">Getting basket completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="404">Basket not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(BasketResponse))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _basketItemService.GetAllAsync(UserId);
            return Ok(result);
        }

        /// <summary>
        /// Returns baskets by user identifier
        /// </summary>
        /// <response code="200">Getting basket completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="404">Basket not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<OrderItemResponse>))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpGet("GetBasketItemsForOrder")]
        public async Task<IActionResult> GetBasketItemsForOrder()
        {
            var result = await _basketItemService.GetBasketItemsForOrderAsync(UserId);
            return Ok(result);
        }

        /// <summary>
        /// Create new basket item
        /// </summary>
        /// <param name="request">Product url slug</param>
        /// <response code="200">Product add completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="404">User or product not found</response>
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
            return Ok(_basketItemLocalizer["AddProductSuccess"].Value);
        }

        /// <summary>
        /// Delete an existing basket
        /// </summary>
        /// <param name="id">Basket identifier</param>
        /// <response code="200">Basket update completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="404">Basket not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteBasketItem(int id)
        {
            await _basketItemService.DeleteAsync(id);
            return Ok(_basketItemLocalizer["DeleteProductSuccess"].Value);
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
            return Ok(_basketItemLocalizer["UpdateProductSuccess"].Value);
        }


    }
}
