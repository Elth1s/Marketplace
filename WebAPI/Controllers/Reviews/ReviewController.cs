using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;
using WebAPI.Interfaces.Reviews;
using WebAPI.ViewModels.Request.Reviews;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Reviews;

namespace WebAPI.Controllers.Reviews
{
    /// <summary>
    /// Review controller class
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private string UserId => User?.FindFirstValue(ClaimTypes.NameIdentifier);

        private readonly IReviewService _reviewService;
        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        /// <summary>
        /// Create new review
        /// </summary>
        /// <param name="request">New review</param>
        /// <response code="200">Review creation completed successfully</response>
        /// <response code="400">Invalid user email or product doesn't ordered</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="404">User or product not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] ReviewRequest request)
        {
            await _reviewService.CreateAsync(request, UserId);
            return Ok("Review created successfully");
        }

        /// <summary>
        /// Get reviews for product
        /// </summary>
        /// <param name="request">Get review data</param>
        /// <response code="200">Getting reviews completed successfully</response>
        /// <response code="404">Product not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(PaginationResponse<ReviewResponse>))]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [HttpGet("GetForProduct")]
        public async Task<IActionResult> GetForProduct([FromQuery] ReviewForProductRequest request)
        {
            var result = await _reviewService.GetByProductSlugAsync(request, UserId);
            return Ok(result);
        }

        /// <summary>
        /// Returns review with the given identifier
        /// </summary>
        /// <param name="id">Review identifier</param>
        /// <response code="200">Getting review completed successfully</response>
        /// <response code="404">Review not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ReviewResponse))]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _reviewService.GetByIdAsync(id, UserId);
            return Ok(result);
        }


        /// <summary>
        /// Change like in review
        /// </summary>
        /// <param name="id">Review identifier</param>
        /// <response code="200">Review like changing completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="404">Review or user not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpPost("Like/{id}")]
        public async Task<IActionResult> Like(int id)
        {
            await _reviewService.Like(id, UserId);
            return Ok("Like changed successfully");
        }

        /// <summary>
        /// Change dislike in review
        /// </summary>
        /// <param name="id">Review identifier</param>
        /// <response code="200">Review like changing completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="404">Review or user not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpPost("Dislike/{id}")]
        public async Task<IActionResult> Diskike(int id)
        {
            await _reviewService.Dislike(id, UserId);
            return Ok("Dislike changed successfully");
        }
    }
}
