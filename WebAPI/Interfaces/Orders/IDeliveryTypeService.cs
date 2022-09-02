using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Request.Orders;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Orders;

namespace WebAPI.Interfaces.Orders
{
    public interface IDeliveryTypeService
    {
        Task<IEnumerable<DeliveryTypeResponse>> GetAsync();
        Task<AdminSearchResponse<DeliveryTypeResponse>> SearchDeliveryTypesAsync(AdminSearchRequest request);
        Task<DeliveryTypeFullInfoResponse> GetByIdAsync(int id);
        Task CreateAsync(DeliveryTypeRequest request);
        Task UpdateAsync(int id, DeliveryTypeRequest request);
        Task DeleteAsync(int id);
        Task DeleteDeliveryTypesAsync(IEnumerable<int> ids);
    }
}
