using WebAPI.ViewModels.Request.Questions;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Questions;

namespace WebAPI.Interfaces.Questions
{
    public interface IQuestionReplyService
    {
        Task CreateAsync(QuestionReplyRequest request, string userId);

        Task<PaginationResponse<QuestionReplyResponse>> GetByQuestion(QuestionReplyForQuestionRequest request);
    }
}
