using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Categories;

namespace WebAPI.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryResponse>> GetAsync();
        Task<AdminSearchResponse<CategoryResponse>> SearchCategoriesAsync(AdminSearchRequest request);
        Task<IEnumerable<CategoryForSelectResponse>> GetForSelectAsync();
        Task<CategoryResponse> GetByIdAsync(int id);
        Task CreateAsync(CategoryRequest request);
        Task UpdateAsync(int id, CategoryRequest request);
        Task DeleteAsync(int id);
        Task DeleteCategoriesAsync(IEnumerable<int> ids);
    }

}
