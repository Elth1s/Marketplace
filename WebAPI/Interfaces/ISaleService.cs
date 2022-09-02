using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Response;

namespace WebAPI.Interfaces
{
    public interface ISaleService
    {
        Task<IEnumerable<SaleResponse>> GetSalesAsync();
        Task<SaleResponse> GetSaleByIdAsync(int saleId);
        Task CreateAsync(SaleRequest request, string userId);
        Task DeleteSaleAsync(int saleId);
    }
}
