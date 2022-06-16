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
    public class FilterNameService : IFilterNameService
    {
        private readonly IRepository<FilterName> _filterNameRepository;
        private readonly IRepository<FilterGroup> _filterGroupRepository;
        private readonly IMapper _mapper;

        public FilterNameService(
            IRepository<FilterName> filterNameRepository,
            IRepository<FilterGroup> filterGroupRepository,
            IMapper mapper
            )
        {
            _filterNameRepository = filterNameRepository;
            _filterGroupRepository = filterGroupRepository;
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

            var filter = _mapper.Map<FilterName>(request);

            await _filterNameRepository.AddAsync(filter);
            await _filterNameRepository.SaveChangesAsync();
        }

        public async Task UpdateFilterNameAsync(int filterNameId, FilterNameRequest request)
        {
            var filterGroup = await _filterGroupRepository.GetByIdAsync(request.FilterGroupId);
            filterGroup.FilterGroupNullChecking();

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
    }
}
