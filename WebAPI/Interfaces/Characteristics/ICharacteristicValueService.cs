using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Request.Characteristics;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Characteristics;

namespace WebAPI.Interfaces.Characteristics
{
    public interface ICharacteristicValueService
    {
        Task<IEnumerable<CharacteristicValueResponse>> GetAsync();
        Task<SearchResponse<CharacteristicValueResponse>> SearchAsync(SellerSearchRequest request, string userId);
        Task<IEnumerable<CharacteristicGroupSellerResponse>> GetCharacteristicsByUserAsync(string userId);
        Task<CharacteristicValueResponse> GetByIdAsync(int id);
        Task CreateAsync(CharacteristicValueRequest request, string userId);
        Task UpdateAsync(int id, CharacteristicValueRequest request, string userId);
        Task DeleteAsync(int id);
        Task DeleteAsync(IEnumerable<int> ids);
    }
}
