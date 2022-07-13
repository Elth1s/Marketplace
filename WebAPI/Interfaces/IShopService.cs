using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Response;

namespace WebAPI.Interfaces
{
    public interface IShopService
    {
        Task<IEnumerable<ShopResponse>> GetShopsAsync();
        Task<AdminSearchResponse<ShopResponse>> SearchShopsAsync(AdminSearchRequest request);
        Task<ShopResponse> GetShopByIdAsync(int shopId);
        Task CreateShopAsync(ShopRequest request, string userId);
        Task UpdateShopAsync(int shopId, ShopRequest request, string userId);
        Task DeleteShopAsync(int shopId);
        Task DeleteShopsAsync(IEnumerable<int> ids);
    }
}
