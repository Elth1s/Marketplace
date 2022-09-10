using AutoMapper;
using DAL;
using DAL.Entities;
using WebAPI.Interfaces.Users;
using WebAPI.Specifications;
using WebAPI.ViewModels.Request.Users;

namespace WebAPI.Services.Users
{
    public class GenderService : IGenderService
    {
        private readonly IRepository<Gender> _genderRepository;
        private readonly IMapper _mapper;

        public GenderService(IRepository<Gender> genderRepository, IMapper mapper)
        {
            _genderRepository = genderRepository;
            _mapper = mapper;
        }


        public async Task<IEnumerable<GenderResponse>> GetGendersAsync()
        {
            var spec = new GenderIncludeFullInfoSpecification();
            var genders = await _genderRepository.ListAsync(spec);

            var response = _mapper.Map<IEnumerable<GenderResponse>>(genders);
            return response;
        }
    }
}
