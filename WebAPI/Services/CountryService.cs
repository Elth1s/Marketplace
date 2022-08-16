using AutoMapper;
using DAL;
using DAL.Constants;
using DAL.Entities;
using WebAPI.Extensions;
using WebAPI.Interfaces;
using WebAPI.Specifications.Countries;
using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Countries;

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
            var spec = new CountryIncludeInfoSpecification();
            var countries = await _countryRepository.ListAsync(spec);

            var response = _mapper.Map<IEnumerable<CountryResponse>>(countries);
            return response;
        }

        public async Task<AdminSearchResponse<CountryResponse>> SearchCountriesAsync(AdminSearchRequest request)
        {
            var spec = new CountrySearchSpecification(request.Name, request.IsAscOrder, request.OrderBy);
            var count = await _countryRepository.CountAsync(spec);
            spec = new CountrySearchSpecification(
                request.Name,
                request.IsAscOrder,
                request.OrderBy,
                (request.Page - 1) * request.RowsPerPage,
                request.RowsPerPage);
            var countries = await _countryRepository.ListAsync(spec);
            var mappedCountries = _mapper.Map<IEnumerable<CountryResponse>>(countries);
            var response = new AdminSearchResponse<CountryResponse>() { Count = count, Values = mappedCountries };

            return response;
        }
        public async Task<CountryFullInfoResponse> GetCountryByIdAsync(int countryId)
        {
            var spec = new CountryIncludeInfoSpecification(countryId);
            var country = await _countryRepository.GetBySpecAsync(spec);
            country.CountryNullChecking();

            var response = _mapper.Map<CountryFullInfoResponse>(country);
            return response;
        }

        public async Task CreateCountryAsync(CountryRequest request)
        {
            var specName = new CountryGetByNameSpecification(request.EnglishName, LanguageId.English);
            var countryEnNameExist = await _countryRepository.GetBySpecAsync(specName);
            if (countryEnNameExist != null)
                countryEnNameExist.CountryWithEnglishNameChecking(nameof(CountryRequest.EnglishName));

            specName = new CountryGetByNameSpecification(request.UkrainianName, LanguageId.Ukrainian);
            var countryUkNameExist = await _countryRepository.GetBySpecAsync(specName);
            if (countryUkNameExist != null)
                countryUkNameExist.CountryWithUkrainianNameChecking(nameof(CountryRequest.UkrainianName));

            var specCode = new CountryGetByCodeSpecification(request.Code);
            var countryCodeExist = await _countryRepository.GetBySpecAsync(specCode);
            countryCodeExist.CountryCodeChecking();


            var result = _mapper.Map<Country>(request);

            await _countryRepository.AddAsync(result);
            await _countryRepository.SaveChangesAsync();
        }

        public async Task UpdateCountryAsync(int countryId, CountryRequest request)
        {
            var spec = new CountryIncludeInfoSpecification(countryId);
            var country = await _countryRepository.GetBySpecAsync(spec);
            country.CountryNullChecking();

            var specName = new CountryGetByNameSpecification(request.EnglishName, LanguageId.English);
            var countryEnNameExist = await _countryRepository.GetBySpecAsync(specName);
            if (countryEnNameExist != null && countryEnNameExist.Id != countryId)
                countryEnNameExist.CountryWithEnglishNameChecking(nameof(CountryRequest.EnglishName));

            specName = new CountryGetByNameSpecification(request.UkrainianName, LanguageId.Ukrainian);
            var countryUkNameExist = await _countryRepository.GetBySpecAsync(specName);
            if (countryUkNameExist != null && countryUkNameExist.Id != countryId)
                countryUkNameExist.CountryWithUkrainianNameChecking(nameof(CountryRequest.UkrainianName));

            var specCode = new CountryGetByCodeSpecification(request.Code);
            var countryCodeExist = await _countryRepository.GetBySpecAsync(specCode);
            if (countryCodeExist != null && countryCodeExist.Id != countryId)
                country.CountryCodeChecking();

            country.CountryTranslations.Clear();

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
