using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Response;

namespace WebAPI.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryResponse>> GetAsync();
        Task<IEnumerable<CategoryForSelectResponse>> GetForSelectAsync();
        Task<CategoryResponse> GetByIdAsync(int id);
        Task CreateAsync(CategoryRequest request);
        Task UpdateAsync(int id, CategoryRequest request);
        Task DeleteAsync(int id);
    }
}
