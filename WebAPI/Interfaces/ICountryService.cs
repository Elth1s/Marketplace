using WebAPI.ViewModels.Request.Countries;
using WebAPI.ViewModels.Response.Countries;

namespace WebAPI.Interfaces
{
    public interface ICountryService
    {
        Task<IEnumerable<CountryResponse>> GetCountriesAsync();
        Task<SearchCountryResponse> SearchCountriesAsync(SearchCountryRequest request);
        Task<CountryResponse> GetCountryByIdAsync(int countryId);
        Task CreateCountryAsync(CountryRequest request);
        Task UpdateCountryAsync(int countryId, CountryRequest request);
        Task DeleteCountryAsync(int countryId);
        Task DeleteCountriesAsync(IEnumerable<int> ids);
    }
}
