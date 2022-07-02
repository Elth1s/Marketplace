using AutoMapper;
using DAL.Entities;
using DAL.Entities.Identity;
using Google.Apis.Auth;
using WebAPI.Constants;
using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Request.Characteristics;
using WebAPI.ViewModels.Request.Filters;
using WebAPI.ViewModels.Request.Products;
using WebAPI.ViewModels.Request.Users;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Categories;
using WebAPI.ViewModels.Response.Characteristics;
using WebAPI.ViewModels.Response.Filters;
using WebAPI.ViewModels.Response.Products;

namespace WebAPI.Mapper
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            //User
            CreateMap<SignUpRequest, AppUser>();

            CreateMap<GoogleJsonWebSignature.Payload, AppUser>()
                .ForMember(u => u.Id, opt => opt.Ignore())
                .ForMember(u => u.FirstName, opt => opt.MapFrom(u => u.GivenName))
                .ForMember(u => u.SecondName, opt => opt.MapFrom(u => u.FamilyName))
                .ForMember(u => u.EmailConfirmed, opt => opt.MapFrom(u => true));

            CreateMap<FacebookResponse, AppUser>()
                .ForMember(u => u.Id, opt => opt.Ignore())
                .ForMember(u => u.EmailConfirmed, opt => opt.MapFrom(u => !string.IsNullOrEmpty(u.Email)));


            CreateMap<AppUser, ProfileResponse>()
                .ForMember(u => u.SecondName, opt => opt.MapFrom(vm => vm.SecondName ?? ""))
                .ForMember(u => u.Email, opt => opt.MapFrom(vm => vm.Email ?? ""))
                .ForMember(u => u.Phone, opt => opt.MapFrom(vm => vm.PhoneNumber ?? ""))
                .ForMember(u => u.Photo, opt => opt.MapFrom(vm => !string.IsNullOrEmpty(vm.Photo) ? string.Concat(ImagePath.RequestUsersImagePath, "/", vm.Photo) : ""))
                .ForMember(u => u.IsEmailConfirmed, opt => opt.MapFrom(vm => vm.EmailConfirmed))
                .ForMember(u => u.IsPhoneConfirmed, opt => opt.MapFrom(vm => vm.PhoneNumberConfirmed));

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

            //BasketItem
            CreateMap<BasketCreateRequest, BasketItem>();
            CreateMap<BasketItem, BasketResponse>();

            //Unit
            CreateMap<UnitRequest, Unit>();
            CreateMap<Unit, UnitResponse>();

        }
    }
}
