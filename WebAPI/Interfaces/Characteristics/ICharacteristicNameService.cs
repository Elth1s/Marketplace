using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Request.Characteristics;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Characteristics;

namespace WebAPI.Interfaces.Characteristics
{
    public interface ICharacteristicNameService
    {
        Task<IEnumerable<CharacteristicNameResponse>> GetAsync(string userId);
        Task<AdminSearchResponse<CharacteristicNameResponse>> SearchAsync(SellerSearchRequest request, string userId);
        Task<CharacteristicNameResponse> GetByIdAsync(int id);
        Task CreateAsync(CharacteristicNameRequest request, string userId);
        Task UpdateAsync(int id, CharacteristicNameRequest request, string userId);
        Task DeleteAsync(int id);
        Task DeleteAsync(IEnumerable<int> ids);
    }
}
