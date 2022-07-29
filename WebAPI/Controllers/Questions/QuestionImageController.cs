using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebAPI.Interfaces.Questions;
using WebAPI.ViewModels.Response.Questions;

namespace WebAPI.Controllers.Questions
{
    /// <summary>
    /// Question image controller class
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionImageController : ControllerBase
    {
        private readonly IQuestionImageService _questionImageService;

        public QuestionImageController(IQuestionImageService questionImageService)
        {
            _questionImageService = questionImageService;
        }

        /// <summary>
        /// Create question image
        /// </summary>
        /// <response code="200">Question image creation completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(QuestionImageResponse))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] string base64)
        {
            var result = await _questionImageService.CreateAsync(base64);
            return Created("Question image created successfully", result);
        }

        /// <summary>
        /// Delete an existing question image
        /// </summary>
        /// <param name="id">Question image identifier</param>
        /// <response code="200">Question image deletion completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="404">Question image not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            await _questionImageService.DeleteAsync(id);
            return Ok("Question image deleted successfully");
        }
    }
}
