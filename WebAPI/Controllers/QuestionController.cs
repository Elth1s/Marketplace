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
    /// The Question controller class.
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController: ControllerBase
    {
        private string UserId => User?.FindFirstValue(ClaimTypes.NameIdentifier);
        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }


        /// <summary>
        /// Returns all questions 
        /// </summary>
        /// <response code="200">Getting question completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="404">Question not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(QuestionResponse))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {

            var result = await _questionService.GetAllAsync();
            return Ok(result);
        }


        /// <summary>
        /// Returns questions by user identifier
        /// </summary>
        /// <response code="200">Getting question completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="404">Question not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(QuestionResponse))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpGet("GetAllById")]
        public async Task<IActionResult> GetAllById()
        {
            var result = await _questionService.GetAllByIdAsync(UserId);
            return Ok(result);
        }

        /// <summary>
        /// Create new question 
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
        public async Task<IActionResult> Create([FromBody] QuestionRequest request)
        {
            await _questionService.CreateAsync(request, UserId);
            return Ok("Question added successfully");
        }

        /// <summary>
        /// Delete an existing question
        /// </summary>
        /// <param name="questionId">Question identifier</param>
        /// <response code="200">Question update completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="404">Question not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpDelete("DeleteQuestion/{questionId}")]
        public async Task<IActionResult> DeleteBasket(int questionId)
        {
            await _questionService.DeleteAsync(questionId);
            return Ok("Question deleted successfully");
        }




    }
}
