using WebAPI.ViewModels.Response.Reviews;

namespace WebAPI.Interfaces.Reviews
{
    public interface IReviewImageService
    {
        Task<ReviewImageResponse> CreateAsync(string base64);
        Task DeleteAsync(int id);
    }
}
