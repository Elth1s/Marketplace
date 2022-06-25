using WebAPI.ViewModels.Request.Products;
using WebAPI.ViewModels.Response.Products;

namespace WebAPI.Interfaces.Products
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponse>> GetAsync();
        Task<ProductResponse> GetByIdAsync(int id);
        Task CreateAsync(ProductCreateRequest request);
        //Task UpdateAsync(int id, ProductUpdateRequest request);
        Task DeleteAsync(int id);
    }
}
