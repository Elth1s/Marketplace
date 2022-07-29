using WebAPI.ViewModels.Request.Reviews;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Reviews;

namespace WebAPI.Interfaces.Reviews
{
    public interface IReviewReplyService
    {
        Task CreateAsync(ReviewReplyRequest request, string userId);

        Task<PaginationResponse<ReviewReplyResponse>> GetByReview(ReviewReplyForReviewRequest request);
    }
}
