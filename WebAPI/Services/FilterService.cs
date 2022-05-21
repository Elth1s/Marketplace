using AutoMapper;
using DAL;
using DAL.Entities;
using WebAPI.Extensions;
using WebAPI.Interfaces;
using WebAPI.Specifications;
using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Response;

namespace WebAPI.Services
{
    public class FilterService : IFilterService
    {
        private readonly IRepository<Filter> _filterRepository;
        private readonly IRepository<FilterGroup> _filterGroupRepository;
        private readonly IMapper _mapper;

        public FilterService(
            IRepository<Filter> filterRepository,
            IRepository<FilterGroup> filterGroupRepository,
            IMapper mapper
            )
        {
            _filterRepository = filterRepository;
            _filterGroupRepository = filterGroupRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FilterResponse>> GetFiltersAsync()
        {
            var spec = new FilterIncludeFullInfoSpecification();
            var filters = await _filterRepository.ListAsync(spec);

            var response = filters.Select(c => _mapper.Map<FilterResponse>(c));
            return response;
        }
        public async Task<FilterResponse> GetFilterByIdAsync(int filterId)
        {
            var spec = new FilterIncludeFullInfoSpecification(filterId);
            var filter = await _filterRepository.GetBySpecAsync(spec);
            filter.FilterNullChecking();

            var response = _mapper.Map<FilterResponse>(filter);
            return response;
        }

        public async Task CreateFilterAsync(FilterRequest request)
        {
            var filterGroup = await _filterGroupRepository.GetByIdAsync(request.FilterGroupId);
            filterGroup.FilterGroupNullChecking();

            var filter = _mapper.Map<Filter>(request);

            await _filterRepository.AddAsync(filter);
            await _filterRepository.SaveChangesAsync();
        }

        public async Task UpdateFilterAsync(int filterId, FilterRequest request)
        {
            var filterGroup = await _filterGroupRepository.GetByIdAsync(request.FilterGroupId);
            filterGroup.FilterGroupNullChecking();

            var filter = await _filterRepository.GetByIdAsync(filterId);
            filter.FilterNullChecking();

            _mapper.Map(request, filter);

            await _filterRepository.UpdateAsync(filter);
            await _filterRepository.SaveChangesAsync();
        }

        public async Task DeleteFilterAsync(int filterId)
        {
            var filter = await _filterRepository.GetByIdAsync(filterId);
            filter.FilterNullChecking();

            await _filterRepository.DeleteAsync(filter);
            await _filterRepository.SaveChangesAsync();
        }
    }
}
