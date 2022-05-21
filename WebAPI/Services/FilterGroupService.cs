using AutoMapper;
using DAL;
using DAL.Entities;
using WebAPI.Extensions;
using WebAPI.Interfaces;
using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Response;

namespace WebAPI.Services
{
    public class FilterGroupService : IFilterGroupService
    {
        private readonly IRepository<FilterGroup> _filterGroupRepository;
        private readonly IMapper _mapper;

        public FilterGroupService(
            IRepository<FilterGroup> filterGroupRepository,
            IMapper mapper
            )
        {
            _filterGroupRepository = filterGroupRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FilterGroupResponse>> GetFilterGroupsAsync()
        {
            var filterGroups = await _filterGroupRepository.ListAsync();

            var response = filterGroups.Select(c => _mapper.Map<FilterGroupResponse>(c));
            return response;
        }
        public async Task<FilterGroupResponse> GetFilterGroupByIdAsync(int filterGroupId)
        {
            var filterGroup = await _filterGroupRepository.GetByIdAsync(filterGroupId);
            filterGroup.FilterGroupNullChecking();

            var response = _mapper.Map<FilterGroupResponse>(filterGroup);
            return response;
        }

        public async Task CreateFilterGroupAsync(FilterGroupRequest request)
        {
            var filterGroup = _mapper.Map<FilterGroup>(request);

            await _filterGroupRepository.AddAsync(filterGroup);
            await _filterGroupRepository.SaveChangesAsync();
        }

        public async Task UpdateFilterGroupAsync(int filterGroupId, FilterGroupRequest request)
        {
            var filterGroup = await _filterGroupRepository.GetByIdAsync(filterGroupId);
            filterGroup.FilterGroupNullChecking();

            _mapper.Map(request, filterGroup);

            await _filterGroupRepository.UpdateAsync(filterGroup);
            await _filterGroupRepository.SaveChangesAsync();
        }

        public async Task DeleteFilterGroupAsync(int filterGroupId)
        {
            var filterGroup = await _filterGroupRepository.GetByIdAsync(filterGroupId);
            filterGroup.FilterGroupNullChecking();

            await _filterGroupRepository.DeleteAsync(filterGroup);
            await _filterGroupRepository.SaveChangesAsync();
        }
    }
}
