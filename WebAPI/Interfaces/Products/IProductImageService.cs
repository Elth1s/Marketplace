using WebAPI.ViewModels.Response.Products;

namespace WebAPI.Interfaces.Products
{
    public interface IProductImageService
    {
        Task<ProductImageResponse> CreateAsync(string base64);
        Task DeleteAsync(int id);
    }
}
