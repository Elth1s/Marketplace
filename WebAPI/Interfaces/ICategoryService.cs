using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Request.Categories;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Categories;
using WebAPI.ViewModels.Response.Filters;

namespace WebAPI.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryResponse>> GetAsync();
        Task<AdminSearchResponse<CategoryResponse>> SearchCategoriesAsync(AdminSearchRequest request);
        Task<IEnumerable<CatalogItemResponse>> GetCatalogAsync();
        Task<IEnumerable<CatalogItemResponse>> GetParentsAsync(string urlSlug);
        Task<CatalogWithProductsResponse> GetCatalogWithProductsAsync(CatalogWithProductsRequest request);
        Task<IEnumerable<FilterNameValuesResponse>> GetFiltersByCategoryAsync(string urlSlug);
        Task<IEnumerable<CategoryForSelectResponse>> GetForSelectAsync();
        Task<CategoryResponse> GetByIdAsync(int id);
        Task CreateAsync(CategoryRequest request);
        Task UpdateAsync(int id, CategoryRequest request);
        Task DeleteAsync(int id);
        Task DeleteCategoriesAsync(IEnumerable<int> ids);
    }

}
