using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;
using WebAPI.Interfaces.Shops;
using WebAPI.ViewModels.Request.Shops;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Shops;

namespace WebAPI.Controllers.Shops
{
    /// <summary>
    /// Shop review controller class
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class ShopReviewController : ControllerBase
    {
        private string UserId => User?.FindFirstValue(ClaimTypes.NameIdentifier);

        private readonly IShopReviewService _shopReviewService;
        private readonly IStringLocalizer<ShopReviewController> _shopReviewLocalizer;

        public ShopReviewController(IShopReviewService shopReviewService, IStringLocalizer<ShopReviewController> shopReviewLocalizer)
        {
            _shopReviewService = shopReviewService;
            _shopReviewLocalizer = shopReviewLocalizer;
        }

        /// <summary>
        /// Create new shop review
        /// </summary>
        /// <param name="request">New shop review</param>
        /// <response code="200">Review creation completed successfully</response>
        /// <response code="400">Invalid user email or no product has been ordered from this shop</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="404">User or shop not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] ShopReviewRequest request)
        {
            await _shopReviewService.CreateAsync(request, UserId);
            return Ok(_shopReviewLocalizer["CreateSuccess"].Value);
        }

        /// <summary>
        /// Get reviews for shop
        /// </summary>
        /// <param name="request">Get review data</param>
        /// <response code="200">Getting reviews completed successfully</response>
        /// <response code="404">Shop not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(PaginationResponse<ShopReviewResponse>))]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [HttpGet("GetForShop")]
        public async Task<IActionResult> GetForShop([FromQuery] ShopReviewForShopRequest request)
        {
            var result = await _shopReviewService.GetByShopIdAsync(request);
            return Ok(result);
        }
    }
}
