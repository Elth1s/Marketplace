using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Response;

namespace WebAPI.Interfaces
{
    public interface IQuestionService
    {
        Task<IEnumerable<QuestionResponse>> GetAllAsync();
        Task<IEnumerable<QuestionResponse>> GetAllByIdAsync(string userId);
        Task CreateAsync(QuestionRequest request, string userId);
        //Task UpdateAsync(int basketId, BasketUpdateRequest request, string userId);
        Task DeleteAsync(int id);
    }
}
