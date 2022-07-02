using AutoMapper;
using DAL;
using DAL.Entities;
using WebAPI.Extensions;
using WebAPI.Interfaces;
using WebAPI.Specifications.Countries;
using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Response;

namespace WebAPI.Services
{
    public class CountryService : ICountryService
    {
        private readonly IRepository<Country> _countryRepository;
        private readonly IMapper _mapper;

        public CountryService(
            IRepository<Country> countryRepository,
            IMapper mapper
            )
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CountryResponse>> GetCountriesAsync()
        {
            var countries = await _countryRepository.ListAsync();

            var response = countries.Select(c => _mapper.Map<CountryResponse>(c));
            return response;
        }

        public async Task<AdminSearchResponse<CountryResponse>> SearchCountriesAsync(AdminSearchRequest request)
        {
            var spec = new CountrySearchSpecification(request.Name, request.IsAscOrder, request.OrderBy);
            var countries = await _countryRepository.ListAsync(spec);
            var mappedCountries = _mapper.Map<IEnumerable<CountryResponse>>(countries);
            var response = new AdminSearchResponse<CountryResponse>() { Count = countries.Count };

            response.Values = mappedCountries.Skip((request.Page - 1) * request.RowsPerPage).Take(request.RowsPerPage);

            return response;
        }
        public async Task<CountryResponse> GetCountryByIdAsync(int countryId)
        {
            var country = await _countryRepository.GetByIdAsync(countryId);
            country.CountryNullChecking();

            var response = _mapper.Map<CountryResponse>(country);
            return response;
        }

        public async Task CreateCountryAsync(CountryRequest request)
        {
            var specCode = new CountryGetByCodeSpecification(request.Code);
            var countryCodeExist = await _countryRepository.GetBySpecAsync(specCode);
            countryCodeExist.CountryCodeChecking();

            var specName = new CountryGetByNameSpecification(request.Name);
            var countryNameExist = await _countryRepository.GetBySpecAsync(specName);
            countryNameExist.CountryNameChecking();


            var result = _mapper.Map<Country>(request);

            await _countryRepository.AddAsync(result);
            await _countryRepository.SaveChangesAsync();
        }

        public async Task UpdateCountryAsync(int countryId, CountryRequest request)
        {
            var country = await _countryRepository.GetByIdAsync(countryId);
            country.CountryNullChecking();

            var spec = new CountryGetByCodeSpecification(request.Code);
            var countryCodeExist = await _countryRepository.GetBySpecAsync(spec);
            if (countryCodeExist != null && countryCodeExist.Id != countryId)
                country.CountryCodeChecking();

            var specName = new CountryGetByNameSpecification(request.Name);
            var countryNameExist = await _countryRepository.GetBySpecAsync(specName);
            if (countryNameExist != null && countryNameExist.Id != countryId)
                countryNameExist.CountryNameChecking();

            _mapper.Map(request, country);

            await _countryRepository.UpdateAsync(country);
            await _countryRepository.SaveChangesAsync();
        }

        public async Task DeleteCountryAsync(int countryId)
        {
            var country = await _countryRepository.GetByIdAsync(countryId);
            country.CountryNullChecking();

            await _countryRepository.DeleteAsync(country);
            await _countryRepository.SaveChangesAsync();
        }

        public async Task DeleteCountriesAsync(IEnumerable<int> ids)
        {
            foreach (var item in ids)
            {
                var country = await _countryRepository.GetByIdAsync(item);
                //country.CountryNullChecking();
                await _countryRepository.DeleteAsync(country);
            }
            await _countryRepository.SaveChangesAsync();
        }
    }
}
