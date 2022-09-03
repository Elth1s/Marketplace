using AutoMapper;
using DAL;
using DAL.Constants;
using DAL.Entities;
using WebAPI.Extensions;
using WebAPI.Interfaces;
using WebAPI.Specifications.Units;
using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Units;

namespace WebAPI.Services
{
    public class UnitService : IUnitService
    {
        private readonly IRepository<Unit> _unitRepository;
        private readonly IMapper _mapper;

        public UnitService(
            IRepository<Unit> unitRepository,
            IMapper mapper
            )
        {
            _unitRepository = unitRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UnitResponse>> GetAllAsync()
        {
            var spec = new UnitIncludeInfoSpecification();
            var units = await _unitRepository.ListAsync(spec);

            var response = _mapper.Map<IEnumerable<UnitResponse>>(units);
            return response;
        }

        public async Task<SearchResponse<UnitResponse>> SearchUnitsAsync(AdminSearchRequest request)
        {
            var spec = new UnitSearchSpecification(request.Name, request.IsAscOrder, request.OrderBy);
            var count = await _unitRepository.CountAsync(spec);
            spec = new UnitSearchSpecification(
                request.Name,
                request.IsAscOrder,
                request.OrderBy,
                (request.Page - 1) * request.RowsPerPage,
                request.RowsPerPage);
            var units = await _unitRepository.ListAsync(spec);
            var mappedUnits = _mapper.Map<IEnumerable<UnitResponse>>(units);
            var response = new SearchResponse<UnitResponse>() { Count = count, Values = mappedUnits };

            return response;
        }

        public async Task<UnitFullInfoResponse> GetByIdAsync(int id)
        {
            var spec = new UnitIncludeInfoSpecification(id);
            var unit = await _unitRepository.GetBySpecAsync(spec);
            unit.UnitNullChecking();

            var response = _mapper.Map<UnitFullInfoResponse>(unit);
            return response;
        }

        public async Task CreateAsync(UnitRequest request)
        {
            var specName = new UnitGetByMeasureSpecification(request.EnglishMeasure, LanguageId.English);
            var unitEnMeasureExist = await _unitRepository.GetBySpecAsync(specName);
            if (unitEnMeasureExist != null)
                unitEnMeasureExist.UnitWithEnglishMeasureChecking(nameof(UnitRequest.EnglishMeasure));

            specName = new UnitGetByMeasureSpecification(request.UkrainianMeasure, LanguageId.Ukrainian);
            var unitUkMeasureExist = await _unitRepository.GetBySpecAsync(specName);
            if (unitUkMeasureExist != null)
                unitUkMeasureExist.UnitWithUkrainianMeasureChecking(nameof(UnitRequest.EnglishMeasure));

            var unit = _mapper.Map<Unit>(request);

            await _unitRepository.AddAsync(unit);
            await _unitRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, UnitRequest request)
        {
            var spec = new UnitIncludeInfoSpecification(id);
            var unit = await _unitRepository.GetBySpecAsync(spec);
            unit.UnitNullChecking();

            var specName = new UnitGetByMeasureSpecification(request.EnglishMeasure, LanguageId.English);
            var unitEnMeasureExist = await _unitRepository.GetBySpecAsync(specName);
            if (unitEnMeasureExist != null && unitEnMeasureExist.Id != unit.Id)
                unitEnMeasureExist.UnitWithEnglishMeasureChecking(nameof(UnitRequest.EnglishMeasure));

            specName = new UnitGetByMeasureSpecification(request.UkrainianMeasure, LanguageId.Ukrainian);
            var unitUkMeasureExist = await _unitRepository.GetBySpecAsync(specName);
            if (unitUkMeasureExist != null && unitUkMeasureExist.Id != unit.Id)
                unitUkMeasureExist.UnitWithUkrainianMeasureChecking(nameof(UnitRequest.UkrainianMeasure));

            unit.UnitTranslations.Clear();

            _mapper.Map(request, unit);

            await _unitRepository.UpdateAsync(unit);
            await _unitRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var unit = await _unitRepository.GetByIdAsync(id);
            unit.UnitNullChecking();

            await _unitRepository.DeleteAsync(unit);
            await _unitRepository.SaveChangesAsync();
        }
        public async Task DeleteUnitsAsync(IEnumerable<int> ids)
        {
            foreach (var item in ids)
            {
                var unit = await _unitRepository.GetByIdAsync(item);
                //unit.UnitNullChecking();
                await _unitRepository.DeleteAsync(unit);
            }
            await _unitRepository.SaveChangesAsync();
        }
    }
}
