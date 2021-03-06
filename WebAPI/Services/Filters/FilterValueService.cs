using AutoMapper;
using DAL;
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
    public class FilterValueService : IFilterValueService
    {
        private readonly IRepository<FilterValue> _filterValueRepository;
        private readonly IRepository<FilterName> _filterNameRepository;
        private readonly IMapper _mapper;

        public FilterValueService(
            IRepository<FilterValue> filterNameRepository,
            IRepository<FilterName> filterGroupRepository,
            IMapper mapper
            )
        {
            _filterValueRepository = filterNameRepository;
            _filterNameRepository = filterGroupRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FilterValueResponse>> GetFiltersValueAsync()
        {
            var spec = new FilterValueIncludeFullInfoSpecification();
            var filters = await _filterValueRepository.ListAsync(spec);

            var response = filters.Select(c => _mapper.Map<FilterValueResponse>(c));
            return response;
        }
        public async Task<FilterValueResponse> GetFilterValueByIdAsync(int filterValueId)
        {
            var spec = new FilterValueIncludeFullInfoSpecification(filterValueId);
            var filter = await _filterValueRepository.GetBySpecAsync(spec);
            filter.FilterValueNullChecking();

            var response = _mapper.Map<FilterValueResponse>(filter);
            return response;
        }

        public async Task CreateFilterValueAsync(FilterValueRequest request)
        {
            var filterName = await _filterNameRepository.GetByIdAsync(request.FilterNameId);
            filterName.FilterNameNullChecking();

            var filter = _mapper.Map<FilterValue>(request);

            await _filterValueRepository.AddAsync(filter);
            await _filterValueRepository.SaveChangesAsync();
        }

        public async Task UpdateFilterValueAsync(int filterValueId, FilterValueRequest request)
        {
            var filterName = await _filterNameRepository.GetByIdAsync(request.FilterNameId);
            filterName.FilterNameNullChecking();

            var filter = await _filterValueRepository.GetByIdAsync(filterValueId);
            filter.FilterValueNullChecking();

            _mapper.Map(request, filter);

            await _filterValueRepository.UpdateAsync(filter);
            await _filterValueRepository.SaveChangesAsync();
        }

        public async Task DeleteFilterValueAsync(int filterValueId)
        {
            var filter = await _filterValueRepository.GetByIdAsync(filterValueId);
            filter.FilterValueNullChecking();

            await _filterValueRepository.DeleteAsync(filter);
            await _filterValueRepository.SaveChangesAsync();
        }

        public async Task<AdminSearchResponse<FilterValueResponse>> SearchAsync(AdminSearchRequest request)
        {
            var spec = new FilterValueSearchSpecification(request.Name, request.IsAscOrder, request.OrderBy);
            var count = await _filterValueRepository.CountAsync(spec);

            spec = new FilterValueSearchSpecification(
                request.Name,
                request.IsAscOrder,
                request.OrderBy,
                (request.Page - 1) * request.RowsPerPage,
                request.RowsPerPage);

            var filterNames = await _filterValueRepository.ListAsync(spec);
            var mappedFilterNames = _mapper.Map<IEnumerable<FilterValueResponse>>(filterNames);
            var response = new AdminSearchResponse<FilterValueResponse>() { Count = count, Values = mappedFilterNames };

            return response;
        }

        public async Task DeleteAsync(IEnumerable<int> ids)
        {
            foreach (var item in ids)
            {
                var filterValue = await _filterValueRepository.GetByIdAsync(item);
                await _filterValueRepository.DeleteAsync(filterValue);
            }
            await _filterValueRepository.SaveChangesAsync();
        }
    }
}
