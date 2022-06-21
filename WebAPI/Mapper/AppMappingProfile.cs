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
                .ForMember(u => u.UserName, opt => opt.MapFrom(vm => vm.Email));

            CreateMap<AppUser, ProfileResponse>()
                .ForMember(u => u.Photo, opt => opt.MapFrom(vm => !string.IsNullOrEmpty(vm.Photo) ? string.Concat(ImagePath.RequestUsersImagePath, "/", vm.Photo) : ""));

            CreateMap<UpdateProfileRequest, AppUser>()
                .ForMember(u => u.Photo, opt => opt.Ignore());


            //Category
            CreateMap<Category, CategoryResponse>()
                .ForMember(u => u.Image, opt => opt.MapFrom(vm => !string.IsNullOrEmpty(vm.Image) ? String.Concat(ImagePath.RequestCategoriesImagePath, "/", vm.Image) : ""));
            CreateMap<Category, CategoryForSelectResponse>();

            CreateMap<CategoryRequest, Category>()
                .ForMember(u => u.Image, opt => opt.Ignore());

            //CharacteristicGroup
            CreateMap<CharacteristicGroupRequest, CharacteristicGroup>();
            CreateMap<CharacteristicGroup, CharacteristicGroupResponse>();

            //CharacteristicName
            CreateMap<CharacteristicNameRequest, CharacteristicName>();
            CreateMap<CharacteristicName, CharacteristicNameResponse>()
                .ForMember(u => u.CharacteristicGroupName, opt => opt.MapFrom(vm => vm.CharacteristicGroup.Name))
                .ForMember(u => u.UnitMeasure, opt => opt.MapFrom(vm => vm.Unit.Measure));

            //CharacteristicValue
            CreateMap<CharacteristicValueRequest, CharacteristicValue>();
            CreateMap<CharacteristicValue, CharacteristicValueResponse>()
                .ForMember(u => u.CharacteristicName, opt => opt.MapFrom(vm => vm.CharacteristicName.Name));

            //Country
            CreateMap<UnitRequest, Country>();
            CreateMap<Country, UnitResponse>();

            //City
            CreateMap<CityRequest, City>();
            CreateMap<City, CityResponse>()
                .ForMember(u => u.CountryName, opt => opt.MapFrom(vm => vm.Country.Name));

            //FilterGroup
            CreateMap<FilterGroupRequest, FilterGroup>();
            CreateMap<FilterGroup, FilterGroupResponse>();

            //FilterName
            CreateMap<FilterNameRequest, FilterName>();
            CreateMap<FilterName, FilterNameResponse>()
                .ForMember(u => u.FilterGroupName, opt => opt.MapFrom(vm => vm.FilterGroup.Name))
                .ForMember(u => u.UnitMeasure, opt => opt.MapFrom(vm => vm.Unit.Measure));

            //FilterValue
            CreateMap<FilterValueRequest, FilterValue>();
            CreateMap<FilterValue, FilterValueResponse>()
                .ForMember(u => u.FilterName, opt => opt.MapFrom(vm => vm.FilterName.Name));

            //Shop
            CreateMap<Shop, ShopResponse>()
                .ForMember(u => u.CountryName, opt => opt.MapFrom(vm => vm.City.Country.Name))
                .ForMember(u => u.CityName, opt => opt.MapFrom(vm => vm.City.Name))
                .ForMember(u => u.Photo, opt => opt.MapFrom(vm => !string.IsNullOrEmpty(vm.Photo) ? String.Concat(ImagePath.RequestShopsImagePath, "/", vm.Photo) : ""));
            CreateMap<ShopRequest, Shop>()
                .ForMember(u => u.Photo, opt => opt.Ignore());

            //Product
            CreateMap<Product, ProductResponse>()
                .ForMember(u => u.ShopName, opt => opt.MapFrom(vm => vm.Shop.Name))
                .ForMember(u => u.StatusName, opt => opt.MapFrom(vm => vm.Status.Name))
                .ForMember(u => u.CategoryName, opt => opt.MapFrom(vm => vm.Category.Name));
            CreateMap<ProductCreateRequest, Product>()
                .ForMember(u => u.Images, opt => opt.Ignore());

            //ProductStatus
            CreateMap<ProductStatusRequest, ProductStatus>();
            CreateMap<ProductStatus, ProductStatusResponse>();

            //Unit
            CreateMap<UnitRequest, Unit>();
            CreateMap<Unit, UnitResponse>();
        }
    }
}
