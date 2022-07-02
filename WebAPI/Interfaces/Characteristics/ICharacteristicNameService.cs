using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Request.Characteristics;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Characteristics;

namespace WebAPI.Interfaces.Characteristics
{
    public interface ICharacteristicNameService
    {
        Task<IEnumerable<CharacteristicNameResponse>> GetAsync();
        Task<AdminSearchResponse<CharacteristicNameResponse>> SearchAsync(AdminSearchRequest request);
        Task<CharacteristicNameResponse> GetByIdAsync(int id);
        Task CreateAsync(CharacteristicNameRequest request);
        Task UpdateAsync(int id, CharacteristicNameRequest request);
        Task DeleteAsync(int id);
        Task DeleteAsync(IEnumerable<int> ids);
    }
}
