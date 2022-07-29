using WebAPI.ViewModels.Request.Orders;
using WebAPI.ViewModels.Response.Orders;

namespace WebAPI.Interfaces.Orders
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
