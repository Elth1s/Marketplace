using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Response;

namespace WebAPI.Interfaces
{
    public interface ICountryService
    {
        Task<IEnumerable<CountryResponse>> GetCountriesAsync();
        Task<CountryResponse> GetCountryByIdAsync(int countryId);
        Task CreateCountryAsync(CountryRequest request);
        Task UpdateCountryAsync(int countryId, CountryRequest request);
        Task DeleteCountryAsync(int countryId);
    }
}
