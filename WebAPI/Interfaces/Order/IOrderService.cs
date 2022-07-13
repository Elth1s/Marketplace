using WebAPI.ViewModels.Request.Order;
using WebAPI.ViewModels.Response.Order;

namespace WebAPI.Interfaces.Order
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderResponse>> GetAsync();
        Task<OrderResponse> GetByIdAsync(int id);
        Task CreateAsync(OrderCreateRequest request);
        Task DeleteAsync(int id);

    }
}
