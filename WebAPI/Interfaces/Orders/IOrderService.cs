using WebAPI.ViewModels.Request.Order;
using WebAPI.ViewModels.Response.Orders;

namespace WebAPI.Interfaces.Orders
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderResponse>> GetAsync();
        Task<OrderResponse> GetByIdAsync(int id);
        Task CreateAsync(OrderCreateRequest request);
        Task DeleteAsync(int id);

    }
}
