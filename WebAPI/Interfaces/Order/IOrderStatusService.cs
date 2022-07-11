using WebAPI.ViewModels.Request.Order;
using WebAPI.ViewModels.Response.Order;

namespace WebAPI.Interfaces.Order
{
    public interface IOrderStatusService
    {
        Task<IEnumerable<OrderStatusResponse>> GetAsync();
        Task<OrderStatusResponse> GetByIdAsync(int id);
        Task CreateAsync(OrderStatusRequest request);
        Task UpdateAsync(int id, OrderStatusRequest request);
        Task DeleteAsync(int id);

    }
}
