using AutoMapper;
using DAL.Entities.Identity;
using WebAPI.ViewModels.Request;

namespace WebAPI.Mapper
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            //User
            CreateMap<SignUpRequest, AppUser>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(vm => vm.Email))
                .ForMember(u => u.PhoneNumber, opt => opt.MapFrom(vm => vm.Phone));
        }
    }
}
