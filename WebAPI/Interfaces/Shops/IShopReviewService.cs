using WebAPI.ViewModels.Request.Shops;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Shops;

namespace WebAPI.Interfaces.Shops
{
    public interface IShopReviewService
    {
        Task CreateAsync(ShopReviewRequest request, string userId);

        Task<PaginationResponse<ShopReviewResponse>> GetByShopIdAsync(ShopReviewForShopRequest request);
    }
}
