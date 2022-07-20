using WebAPI.ViewModels.Request;

namespace WebAPI.Interfaces
{
    public interface IQuestionService
    {

        //Task<IEnumerable<BasketResponse>> GetAllAsync(string userId);
        Task CreateAsync(QuestionRequest request, string userId);
        //Task UpdateAsync(int basketId, BasketUpdateRequest request, string userId);
        Task DeleteAsync(int id);
    }
}
