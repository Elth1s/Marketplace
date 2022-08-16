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
            var spec = new FilterGroupIncludeInfoSpecification();
            var filterGroups = await _filterGroupRepository.ListAsync(spec);

            var response = _mapper.Map<IEnumerable<FilterGroupResponse>>(filterGroups);
            return response;
        }
        public async Task<FilterGroupFullInfoResponse> GetFilterGroupByIdAsync(int filterGroupId)
        {
            var spec = new FilterGroupIncludeInfoSpecification(filterGroupId);
            var filterGroup = await _filterGroupRepository.GetBySpecAsync(spec);
            filterGroup.FilterGroupNullChecking();

            var response = _mapper.Map<FilterGroupFullInfoResponse>(filterGroup);
            return response;
        }

        public async Task CreateFilterGroupAsync(FilterGroupRequest request)
        {
            var specName = new FilterGroupGetByNameSpecification(request.EnglishName, LanguageId.English);
            var filterGroupEnNameExist = await _filterGroupRepository.GetBySpecAsync(specName);
            if (filterGroupEnNameExist != null)
                filterGroupEnNameExist.FilterGroupWithEnglishNameChecking(nameof(CountryRequest.EnglishName));

            specName = new FilterGroupGetByNameSpecification(request.UkrainianName, LanguageId.Ukrainian);
            var filterGroupUkNameExist = await _filterGroupRepository.GetBySpecAsync(specName);
            if (filterGroupUkNameExist != null)
                filterGroupUkNameExist.FilterGroupWithUkrainianNameChecking(nameof(CountryRequest.UkrainianName));

            var filterGroup = _mapper.Map<FilterGroup>(request);

            await _filterGroupRepository.AddAsync(filterGroup);
            await _filterGroupRepository.SaveChangesAsync();
        }

        public async Task UpdateFilterGroupAsync(int filterGroupId, FilterGroupRequest request)
        {
            var spec = new FilterGroupIncludeInfoSpecification(filterGroupId);
            var filterGroup = await _filterGroupRepository.GetBySpecAsync(spec);
            filterGroup.FilterGroupNullChecking();

            var specName = new FilterGroupGetByNameSpecification(request.EnglishName, LanguageId.English);
            var filterGroupEnNameExist = await _filterGroupRepository.GetBySpecAsync(specName);
            if (filterGroupEnNameExist != null && filterGroupEnNameExist.Id != filterGroup.Id)
                filterGroupEnNameExist.FilterGroupWithEnglishNameChecking(nameof(FilterGroupRequest.EnglishName));

            specName = new FilterGroupGetByNameSpecification(request.UkrainianName, LanguageId.Ukrainian);
            var filterGroupUkNameExist = await _filterGroupRepository.GetBySpecAsync(specName);
            if (filterGroupUkNameExist != null && filterGroupUkNameExist.Id != filterGroup.Id)
                filterGroupUkNameExist.FilterGroupWithUkrainianNameChecking(nameof(FilterGroupRequest.UkrainianName));

            filterGroup.FilterGroupTranslations.Clear();
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
            var count = await _filterGroupRepository.CountAsync(spec);
            spec = new FilterGroupSearchSpecification(
                request.Name,
                request.IsAscOrder,
                request.OrderBy,
                (request.Page - 1) * request.RowsPerPage,
                request.RowsPerPage);

            var filterGroups = await _filterGroupRepository.ListAsync(spec);

            var mappedGroups = _mapper.Map<IEnumerable<FilterGroupResponse>>(filterGroups);
            var response = new AdminSearchResponse<FilterGroupResponse>() { Count = count, Values = mappedGroups };

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
