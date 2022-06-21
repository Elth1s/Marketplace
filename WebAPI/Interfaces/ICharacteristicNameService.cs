
using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Response;

namespace WebAPI.Interfaces
{
    public interface ICharacteristicNameService
    {
        Task<IEnumerable<CharacteristicNameResponse>> GetAsync();
        Task<CharacteristicNameResponse> GetByIdAsync(int id);
        Task CreateAsync(CharacteristicNameRequest request);
        Task UpdateAsync(int id, CharacteristicNameRequest request);
        Task DeleteAsync(int id);
    }
}
