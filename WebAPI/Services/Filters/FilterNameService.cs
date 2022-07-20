using AutoMapper;
using DAL;
using DAL.Entities;
using WebAPI.Exceptions;
using WebAPI.Extensions;
using WebAPI.Interfaces.Filters;
using WebAPI.Resources;
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
        public async Task<FilterNameResponse> GetFilterNameByIdAsync(int filterNameId)
        {
            var spec = new FilterNameIncludeFullInfoSpecification(filterNameId);
            var filter = await _filterNameRepository.GetBySpecAsync(spec);
            filter.FilterNameNullChecking();

            var response = _mapper.Map<FilterNameResponse>(filter);
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

            var spec = new FilterNameGetByNameAndUnitIdSpecification(request.Name, request.UnitId);
            if (await _filterNameRepository.GetBySpecAsync(spec) != null)
                throw new AppValidationException(new ValidationError(nameof(FilterName.UnitId), ErrorMessages.FilterNameUnitNotUnique));

            var filter = _mapper.Map<FilterName>(request);

            await _filterNameRepository.AddAsync(filter);
            await _filterNameRepository.SaveChangesAsync();
        }

        public async Task UpdateFilterNameAsync(int filterNameId, FilterNameRequest request)
        {
            var filterGroup = await _filterGroupRepository.GetByIdAsync(request.FilterGroupId);
            filterGroup.FilterGroupNullChecking();

            if (request.UnitId != null)
            {
                var unit = await _unitRepository.GetByIdAsync(request.UnitId);
                unit.UnitNullChecking();
            }

            var spec = new FilterNameGetByNameAndUnitIdSpecification(request.Name, request.UnitId.Value);
            if (await _filterNameRepository.GetBySpecAsync(spec) != null)
                throw new AppValidationException(new ValidationError(nameof(FilterName.UnitId), ErrorMessages.FilterNameUnitNotUnique));

            var filter = await _filterNameRepository.GetByIdAsync(filterNameId);
            filter.FilterNameNullChecking();

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

        public async Task<AdminSearchResponse<FilterNameResponse>> SearchAsync(AdminSearchRequest request)
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
            var response = new AdminSearchResponse<FilterNameResponse>() { Count = count, Values = mappedFilterNames };
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
