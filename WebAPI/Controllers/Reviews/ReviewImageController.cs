using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebAPI.Interfaces.Reviews;
using WebAPI.ViewModels.Response.Reviews;

namespace WebAPI.Controllers.Reviews
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewImageController : ControllerBase
    {
        private readonly IReviewImageService _reviewImageService;

        public ReviewImageController(IReviewImageService reviewImageService)
        {
            _reviewImageService = reviewImageService;
        }

        /// <summary>
        /// Create review image
        /// </summary>
        /// <response code="200">Review image creation completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ReviewImageResponse))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] string base64)
        {
            var result = await _reviewImageService.CreateAsync(base64);
            return Created("Review image created successfully", result);
        }

        /// <summary>
        /// Delete an existing review image
        /// </summary>
        /// <param name="id">Review image identifier</param>
        /// <response code="200">Review image deletion completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="404">Review image not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            await _reviewImageService.DeleteAsync(id);
            return Ok("Review image deleted successfully");
        }
    }
}
