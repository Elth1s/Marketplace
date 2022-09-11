using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Sales;

namespace WebAPI.Interfaces
{
    public interface ISaleService
    {
        Task<IEnumerable<SaleResponse>> GetSalesAsync();
        Task<SaleResponse> GetSaleAsync(int saleId);
        Task<SaleFullInfoResponse> GetSaleByIdAsync(int saleId);
        Task<SearchResponse<SaleResponse>> SearchSalesAsync(AdminSearchRequest request);
        Task CreateAsync(SaleRequest request);
        Task UpdateAsync(int id, SaleRequest request);
        Task DeleteSaleAsync(int saleId);
        Task DeleteSalesAsync(IEnumerable<int> ids);
    }
}
