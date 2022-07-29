using WebAPI.ViewModels.Response.Questions;

namespace WebAPI.Interfaces.Questions
{
    public interface IQuestionImageService
    {
        Task<QuestionImageResponse> CreateAsync(string base64);
        Task DeleteAsync(int id);
    }
}