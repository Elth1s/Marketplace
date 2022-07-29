using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;
using WebAPI.Interfaces.Questions;
using WebAPI.ViewModels.Request.Questions;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Questions;

namespace WebAPI.Controllers.Questions
{
    /// <summary>
    /// Question reply controller class
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionReplyController : ControllerBase
    {
        private string UserId => User?.FindFirstValue(ClaimTypes.NameIdentifier);
        private readonly IQuestionReplyService _questionReplyService;
        public QuestionReplyController(IQuestionReplyService questionReplyService)
        {
            _questionReplyService = questionReplyService;
        }

        /// <summary>
        /// Create new question reply
        /// </summary>
        /// <param name="request">New question reply</param>
        /// <response code="200">Question reply creation completed successfully</response>
        /// <response code="400">Invalid user email</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="404">User or question not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] QuestionReplyRequest request)
        {
            await _questionReplyService.CreateAsync(request, UserId);
            return Ok("Question reply created successfully");
        }

        /// <summary>
        /// Get question replies for question
        /// </summary>
        /// <param name="request">Get reply data</param>
        /// <response code="200">Getting question replies completed successfully</response>
        /// <response code="404">Question not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(PaginationResponse<QuestionReplyResponse>))]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [HttpGet("GetByQuestion")]
        public async Task<IActionResult> GetByQuestion([FromQuery] QuestionReplyForQuestionRequest request)
        {
            var result = await _questionReplyService.GetByQuestion(request);
            return Ok(result);
        }
    }
}
