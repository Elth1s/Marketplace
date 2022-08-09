using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;
using WebAPI.Interfaces.Questions;
using WebAPI.ViewModels.Request.Questions;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Questions;

namespace WebAPI.Controllers.Questions
{
    /// <summary>
    /// The Question controller class.
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private string UserId => User?.FindFirstValue(ClaimTypes.NameIdentifier);

        private readonly IStringLocalizer<QuestionController> _questionLocalizer;
        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionService questionService,
            IStringLocalizer<QuestionController> questionLocalizer)
        {
            _questionService = questionService;
            _questionLocalizer = questionLocalizer;
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
        /// <param name="request">New question</param>
        /// <response code="200">Product add completed successfully</response>
        /// <response code="400">Invalid user email</response>
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
        public async Task<IActionResult> Create([FromBody] QuestionRequest request)
        {
            await _questionService.CreateAsync(request, UserId);
            return Ok(_questionLocalizer["CreateSuccess"].Value);
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
            return Ok(_questionLocalizer["DeleteSuccess"].Value);
        }


        /// <summary>
        /// Get questions for product
        /// </summary>
        /// <param name="request">Get question data</param>
        /// <response code="200">Getting questions completed successfully</response>
        /// <response code="404">Product not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(PaginationResponse<QuestionResponse>))]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [HttpGet("GetForProduct")]
        public async Task<IActionResult> GetForProduct([FromQuery] QuestionForProductRequest request)
        {
            var result = await _questionService.GetByProductSlugAsync(request, UserId);
            return Ok(result);
        }

        /// <summary>
        /// Returns question with the given identifier
        /// </summary>
        /// <param name="id">Question identifier</param>
        /// <response code="200">Getting question completed successfully</response>
        /// <response code="404">Question not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(QuestionResponse))]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _questionService.GetByIdAsync(id, UserId);
            return Ok(result);
        }


        /// <summary>
        /// Change like in question
        /// </summary>
        /// <param name="id">Question identifier</param>
        /// <response code="200">Question like changing completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="404">Question or user not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpPost("Like/{id}")]
        public async Task<IActionResult> Like(int id)
        {
            await _questionService.Like(id, UserId);
            return Ok(_questionLocalizer["LikeSuccess"].Value);
        }

        /// <summary>
        /// Change dislike in question
        /// </summary>
        /// <param name="id">Question identifier</param>
        /// <response code="200">Question like changing completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="404">Question or user not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpPost("Dislike/{id}")]
        public async Task<IActionResult> Diskike(int id)
        {
            await _questionService.Dislike(id, UserId);
            return Ok(_questionLocalizer["DislikeSuccess"].Value);
        }

    }
}
