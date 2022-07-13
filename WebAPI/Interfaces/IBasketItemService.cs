using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Response;

namespace WebAPI.Interfaces
{
    public interface IBasketItemService
    {
        Task<IEnumerable<BasketResponse>> GetAllAsync(string userId);
        Task CreateAsync(BasketCreateRequest request, string userId);
        Task UpdateAsync(int basketId, BasketUpdateRequest request, string userId);
        Task DeleteAsync(int id);
    }
}
