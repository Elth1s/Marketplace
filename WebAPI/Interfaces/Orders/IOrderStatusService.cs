using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Request.Orders;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Orders;

namespace WebAPI.Interfaces.Orders
{
    public interface IOrderStatusService
    {
        Task<IEnumerable<OrderStatusResponse>> GetAsync();
        Task<AdminSearchResponse<OrderStatusResponse>> SearchOrderStatusesAsync(AdminSearchRequest request);
        Task<OrderStatusFullInfoResponse> GetByIdAsync(int id);
        Task CreateAsync(OrderStatusRequest request);
        Task UpdateAsync(int id, OrderStatusRequest request);
        Task DeleteAsync(int id);
        Task DeleteOrderStatusesAsync(IEnumerable<int> ids);

    }
}
