using AutoMapper;
using DAL.Entities;
using DAL.Entities.Identity;
using WebAPI.Constants;
using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Response;

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

            CreateMap<AppUser, ProfileResponse>()
                .ForMember(u => u.Phone, opt => opt.MapFrom(vm => vm.PhoneNumber))
                .ForMember(u => u.Photo, opt => opt.MapFrom(vm => !string.IsNullOrEmpty(vm.Photo) ? String.Concat(ImagePath.RequestUsersImagePath, "/", vm.Photo) : ""));

            CreateMap<UpdateProfileRequest, AppUser>()
                .ForMember(u => u.Photo, opt => opt.Ignore());

            //Country
            CreateMap<CountryRequest, Country>();
            CreateMap<Country, CountryResponse>();

            //Country
            CreateMap<CityRequest, City>();
            CreateMap<City, CityResponse>()
                .ForMember(u => u.CountryName, opt => opt.MapFrom(vm => vm.Country.Name));

            //Shop
            CreateMap<Shop, ShopResponse>()
                .ForMember(u => u.CountryName, opt => opt.MapFrom(vm => vm.City.Country.Name))
                .ForMember(u => u.CityName, opt => opt.MapFrom(vm => vm.City.Name))
                .ForMember(u => u.Photo, opt => opt.MapFrom(vm => !string.IsNullOrEmpty(vm.Photo) ? String.Concat(ImagePath.RequestShopsImagePath, "/", vm.Photo) : ""));
            CreateMap<ShopRequest, Shop>()
                .ForMember(u => u.Photo, opt => opt.Ignore());
        }
    }
}
