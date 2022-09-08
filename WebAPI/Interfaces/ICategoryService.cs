using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Request.Categories;
using WebAPI.ViewModels.Request.Products;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Categories;
using WebAPI.ViewModels.Response.Filters;
using WebAPI.ViewModels.Response.Products;

namespace WebAPI.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryResponse>> GetAsync();
        Task<SearchResponse<CategoryResponse>> SearchCategoriesAsync(AdminSearchRequest request);
        Task<IEnumerable<CatalogItemResponse>> GetCatalogAsync();
        Task<IEnumerable<FullCatalogItemResponse>> GetFullCatalogAsync();
        Task<IEnumerable<CatalogItemResponse>> GetParentsAsync(string urlSlug);
        Task<CatalogWithProductsResponse> GetCatalogWithProductsAsync(CatalogWithProductsRequest request, string userId);
        Task<IEnumerable<FullCatalogItemResponse>> GetCategoriesByProductsAsync(SearchProductsRequest request);
        Task<IEnumerable<ProductCatalogResponse>> GetMoreProductsAsync(CatalogWithProductsRequest request, string userId);
        Task<IEnumerable<FilterNameValuesResponse>> GetFiltersByCategoryAsync(string urlSlug);
        Task<IEnumerable<FilterNameValuesResponse>> GetFiltersByCategoryAsync(int id);
        Task<IEnumerable<FilterGroupSellerResponse>> GetFiltersByCategoryIdAsync(int id);
        Task<IEnumerable<CategoryForSelectResponse>> GetForSelectAsync();
        Task<IEnumerable<CategoryForSelectResponse>> GetCategoriesWithoutChildrenAsync();
        Task<CategoryFullInfoResponse> GetByIdAsync(int id);
        Task CreateAsync(CategoryRequest request);
        Task UpdateAsync(int id, CategoryRequest request);
        Task DeleteAsync(int id);
        Task DeleteCategoriesAsync(IEnumerable<int> ids);
    }

}
