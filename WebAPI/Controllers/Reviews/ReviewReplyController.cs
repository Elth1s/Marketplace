using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;
using WebAPI.Interfaces.Reviews;
using WebAPI.ViewModels.Request.Reviews;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Reviews;

namespace WebAPI.Controllers.Reviews
{
    /// <summary>
    /// Review reply controller class
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewReplyController : ControllerBase
    {
        private string UserId => User?.FindFirstValue(ClaimTypes.NameIdentifier);

        private readonly IStringLocalizer<ReviewReplyController> _reviewReplyLocalizer;
        private readonly IReviewReplyService _reviewReplyService;
        public ReviewReplyController(IReviewReplyService reviewReplyService,
            IStringLocalizer<ReviewReplyController> reviewReplyLocalizer)
        {
            _reviewReplyService = reviewReplyService;
            _reviewReplyLocalizer = reviewReplyLocalizer;
        }

        /// <summary>
        /// Create new review reply
        /// </summary>
        /// <param name="request">New review reply</param>
        /// <response code="200">Review reply creation completed successfully</response>
        /// <response code="400">Invalid user email</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="404">User or review not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] ReviewReplyRequest request)
        {
            await _reviewReplyService.CreateAsync(request, UserId);
            return Ok(_reviewReplyLocalizer["CreateSuccess"].Value);
        }

        /// <summary>
        /// Get review replies for review
        /// </summary>
        /// <param name="request">Get reply data</param>
        /// <response code="200">Getting review replies completed successfully</response>
        /// <response code="404">Review not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(PaginationResponse<ReviewReplyResponse>))]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [HttpGet("GetByReview")]
        public async Task<IActionResult> GetByReview([FromQuery] ReviewReplyForReviewRequest request)
        {
            var result = await _reviewReplyService.GetByReview(request);
            return Ok(result);
        }
    }
}
