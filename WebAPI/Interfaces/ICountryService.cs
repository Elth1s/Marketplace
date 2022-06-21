using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Response;

namespace WebAPI.Interfaces
{
    public interface ICountryService
    {
        Task<IEnumerable<UnitResponse>> GetCountriesAsync();
        Task<UnitResponse> GetCountryByIdAsync(int countryId);
        Task CreateCountryAsync(UnitRequest request);
        Task UpdateCountryAsync(int countryId, UnitRequest request);
        Task DeleteCountryAsync(int countryId);
    }
}
