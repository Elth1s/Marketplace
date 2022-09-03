using AutoMapper;
using DAL;
using DAL.Constants;
using DAL.Entities;
using WebAPI.Extensions;
using WebAPI.Interfaces.Filters;
using WebAPI.Specifications.Filters;
using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Request.Filters;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Filters;

namespace WebAPI.Services.Filters
{
    public class FilterNameService : IFilterNameService
    {
        private readonly IRepository<FilterName> _filterNameRepository;
        private readonly IRepository<FilterGroup> _filterGroupRepository;
        private readonly IRepository<Unit> _unitRepository;
        private readonly IMapper _mapper;

        public FilterNameService(
            IRepository<FilterName> filterNameRepository,
            IRepository<FilterGroup> filterGroupRepository,
            IRepository<Unit> unitRepository,
            IMapper mapper
            )
        {
            _filterNameRepository = filterNameRepository;
            _filterGroupRepository = filterGroupRepository;
            _unitRepository = unitRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FilterNameResponse>> GetFiltersNameAsync()
        {
            var spec = new FilterNameIncludeFullInfoSpecification();
            var filters = await _filterNameRepository.ListAsync(spec);

            var response = filters.Select(c => _mapper.Map<FilterNameResponse>(c));
            return response;
        }
        public async Task<FilterNameFullInfoResponse> GetFilterNameByIdAsync(int filterNameId)
        {
            var spec = new FilterNameIncludeFullInfoSpecification(filterNameId);
            var filter = await _filterNameRepository.GetBySpecAsync(spec);
            filter.FilterNameNullChecking();

            var response = _mapper.Map<FilterNameFullInfoResponse>(filter);
            return response;
        }

        public async Task CreateFilterNameAsync(FilterNameRequest request)
        {
            var filterGroup = await _filterGroupRepository.GetByIdAsync(request.FilterGroupId);
            filterGroup.FilterGroupNullChecking();

            if (request.UnitId != null)
            {
                var unit = await _unitRepository.GetByIdAsync(request.UnitId);
                unit.UnitNullChecking();
            }
            var specName = new FilterNameGetByNameAndUnitIdSpecification(request.EnglishName, filterGroup.Id, request.UnitId, LanguageId.English);
            var filterNameEnNameExist = await _filterNameRepository.GetBySpecAsync(specName);
            if (filterNameEnNameExist != null)
                filterNameEnNameExist.FilterNameWithEnglishNameChecking(nameof(FilterNameRequest.EnglishName));

            specName = new FilterNameGetByNameAndUnitIdSpecification(request.UkrainianName, filterGroup.Id, request.UnitId, LanguageId.Ukrainian);
            var filterNameUkNameExist = await _filterNameRepository.GetBySpecAsync(specName);
            if (filterNameUkNameExist != null)
                filterNameUkNameExist.FilterNameWithUkrainianNameChecking(nameof(FilterNameRequest.UkrainianName));

            var filter = _mapper.Map<FilterName>(request);

            await _filterNameRepository.AddAsync(filter);
            await _filterNameRepository.SaveChangesAsync();
        }

        public async Task UpdateFilterNameAsync(int filterNameId, FilterNameRequest request)
        {
            var spec = new FilterNameIncludeFullInfoSpecification(filterNameId);
            var filter = await _filterNameRepository.GetBySpecAsync(spec);
            filter.FilterNameNullChecking();

            var filterGroup = await _filterGroupRepository.GetByIdAsync(request.FilterGroupId);
            filterGroup.FilterGroupNullChecking();

            if (request.UnitId != null)
            {
                var unit = await _unitRepository.GetByIdAsync(request.UnitId);
                unit.UnitNullChecking();
            }

            var specName = new FilterNameGetByNameAndUnitIdSpecification(
                request.EnglishName,
                filterGroup.Id,
                request.UnitId,
                LanguageId.English);
            var filterNameEnNameExist = await _filterNameRepository.GetBySpecAsync(specName);
            if (filterNameEnNameExist != null && filterNameEnNameExist.Id != filter.Id)
                filterNameEnNameExist.FilterNameWithEnglishNameChecking(nameof(FilterNameRequest.EnglishName));

            specName = new FilterNameGetByNameAndUnitIdSpecification(
                request.UkrainianName,
                filterGroup.Id,
                request.UnitId,
                LanguageId.Ukrainian);
            var filterNameUkNameExist = await _filterNameRepository.GetBySpecAsync(specName);
            if (filterNameUkNameExist != null && filterNameUkNameExist.Id != filter.Id)
                filterNameUkNameExist.FilterNameWithUkrainianNameChecking(nameof(FilterNameRequest.UkrainianName));

            filter.FilterNameTranslations.Clear();
            _mapper.Map(request, filter);

            await _filterNameRepository.UpdateAsync(filter);
            await _filterNameRepository.SaveChangesAsync();
        }

        public async Task DeleteFilterNameAsync(int filterNameId)
        {
            var filter = await _filterNameRepository.GetByIdAsync(filterNameId);
            filter.FilterNameNullChecking();

            await _filterNameRepository.DeleteAsync(filter);
            await _filterNameRepository.SaveChangesAsync();
        }

        public async Task<SearchResponse<FilterNameResponse>> SearchAsync(AdminSearchRequest request)
        {
            var spec = new FilterNameSearchSpecification(request.Name, request.IsAscOrder, request.OrderBy);
            var count = await _filterNameRepository.CountAsync(spec);
            spec = new FilterNameSearchSpecification(
                request.Name,
                request.IsAscOrder,
                request.OrderBy,
                (request.Page - 1) * request.RowsPerPage,
                request.RowsPerPage
                );

            var filterNames = await _filterNameRepository.ListAsync(spec);
            var mappedFilterNames = _mapper.Map<IEnumerable<FilterNameResponse>>(filterNames);
            var response = new SearchResponse<FilterNameResponse>() { Count = count, Values = mappedFilterNames };
            return response;
        }

        public async Task DeleteAsync(IEnumerable<int> ids)
        {
            foreach (var item in ids)
            {
                var filterName = await _filterNameRepository.GetByIdAsync(item);
                await _filterNameRepository.DeleteAsync(filterName);
            }
            await _filterNameRepository.SaveChangesAsync();
        }
    }
}
