using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Countries;

namespace WebAPI.Interfaces
{
    public interface ICountryService
    {
        Task<IEnumerable<CountryResponse>> GetCountriesAsync();
        Task<SearchResponse<CountryResponse>> SearchCountriesAsync(AdminSearchRequest request);
        Task<CountryFullInfoResponse> GetCountryByIdAsync(int countryId);
        Task CreateCountryAsync(CountryRequest request);
        Task UpdateCountryAsync(int countryId, CountryRequest request);
        Task DeleteCountryAsync(int countryId);
        Task DeleteCountriesAsync(IEnumerable<int> ids);
    }
}
