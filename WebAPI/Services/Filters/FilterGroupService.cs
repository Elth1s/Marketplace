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

        public async Task<AdminSearchResponse<FilterGroupResponse>> SearchAsync(AdminSearchRequest request)
        {
            var spec = new FilterGroupSearchSpecification(request.Name, request.IsAscOrder, request.OrderBy);
            var filterGroups = await _filterGroupRepository.ListAsync(spec);
            var mappedGroups = _mapper.Map<IEnumerable<FilterGroupResponse>>(filterGroups);
            var response = new AdminSearchResponse<FilterGroupResponse>() { Count = filterGroups.Count };

            response.Values = mappedGroups.Skip((request.Page - 1) * request.RowsPerPage).Take(request.RowsPerPage);

            return response;
        }

        public async Task DeleteAsync(IEnumerable<int> ids)
        {
            foreach (var item in ids)
            {
                var group = await _filterGroupRepository.GetByIdAsync(item);
                await _filterGroupRepository.DeleteAsync(group);
            }
            await _filterGroupRepository.SaveChangesAsync();
        }
    }
}
