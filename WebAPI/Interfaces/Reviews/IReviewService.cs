using WebAPI.ViewModels.Request.Reviews;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Reviews;

namespace WebAPI.Interfaces.Reviews
{
    public interface IReviewService
    {
        Task CreateAsync(ReviewRequest request, string userId);

        Task<IEnumerable<ReviewResponse>> GetAsync();

        Task<ReviewResponse> GetByIdAsync(int id, string userId);

        Task<PaginationResponse<ReviewResponse>> GetByProductSlugAsync(ReviewForProductRequest request, string userId);

        Task Like(int id, string userId);
        Task Dislike(int id, string userId);
    }
}
