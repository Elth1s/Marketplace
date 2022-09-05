using WebAPI.ViewModels.Request.Baskets;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Orders;

namespace WebAPI.Interfaces
{
    public interface IBasketItemService
    {
        Task<IEnumerable<BasketResponse>> GetAllAsync(string userId);
        Task<IEnumerable<OrderItemResponse>> GetBasketItemsForOrderAsync(string userId);
        Task CreateAsync(BasketCreateRequest request, string userId);
        Task UpdateAsync(int basketId, int count);
        Task DeleteAsync(int id);
    }
}
