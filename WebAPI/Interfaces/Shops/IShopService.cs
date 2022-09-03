using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Request.Shops;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Shops;
using WebAPI.ViewModels.Response.Users;

namespace WebAPI.Interfaces.Shops
{
    public interface IShopService
    {
        Task<IEnumerable<ShopResponse>> GetShopsAsync();
        Task<SearchResponse<ShopResponse>> SearchShopsAsync(AdminSearchRequest request);
        Task<ShopResponse> GetShopByIdAsync(int shopId);
        Task<ShopInfoFromProductResponse> ShopInfoFromProductAsync(int shopId);
        Task<ShopPageInfoResponse> GetShopInfoAsync(int shopId);
        Task<AuthResponse> CreateShopAsync(ShopRequest request, string userId, string ipAddress);
        Task UpdateShopAsync(int shopId, ShopRequest request, string userId);
        Task DeleteShopAsync(int shopId);
        Task DeleteShopsAsync(IEnumerable<int> ids);
    }
}
