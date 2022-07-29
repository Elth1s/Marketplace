using WebAPI.ViewModels.Request.Questions;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Questions;

namespace WebAPI.Interfaces.Questions
{
    public interface IQuestionService
    {
        Task CreateAsync(QuestionRequest request, string userId);
        Task<IEnumerable<QuestionResponse>> GetAllAsync();
        Task<IEnumerable<QuestionResponse>> GetAllByIdAsync(string userId);

        Task<QuestionResponse> GetByIdAsync(int id, string userId);

        Task<PaginationResponse<QuestionResponse>> GetByProductSlugAsync(QuestionForProductRequest request, string userId);
        Task Like(int id, string userId);
        Task Dislike(int id, string userId);

        Task DeleteAsync(int id);
    }
}
