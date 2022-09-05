using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Response;

namespace WebAPI.Interfaces
{
    public interface ISaleService
    {
        Task<IEnumerable<SaleResponse>> GetSalesAsync();
        Task<SaleResponse> GetSaleByIdAsync(int saleId);
        Task CreateAsync(SaleRequest request);
        Task UpdateAsync(int id, SaleRequest request);
        Task DeleteSaleAsync(int saleId);
        Task DeleteSalesAsync(IEnumerable<int> ids);
    }
}
