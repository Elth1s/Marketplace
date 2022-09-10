using AutoMapper;
using DAL.Constants;
using DAL.Entities;
using Google.Apis.Auth;
using WebAPI.Constants;
using WebAPI.Helpers;
using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Request.Categories;
using WebAPI.ViewModels.Request.Characteristics;
using WebAPI.ViewModels.Request.Filters;
using WebAPI.ViewModels.Request.Orders;
using WebAPI.ViewModels.Request.Products;
using WebAPI.ViewModels.Request.Questions;
using WebAPI.ViewModels.Request.Reviews;
using WebAPI.ViewModels.Request.Shops;
using WebAPI.ViewModels.Request.Users;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Categories;
using WebAPI.ViewModels.Response.Characteristics;
using WebAPI.ViewModels.Response.Cities;
using WebAPI.ViewModels.Response.Countries;
using WebAPI.ViewModels.Response.Filters;
using WebAPI.ViewModels.Response.Orders;
using WebAPI.ViewModels.Response.Products;
using WebAPI.ViewModels.Response.Questions;
using WebAPI.ViewModels.Response.Reviews;
using WebAPI.ViewModels.Response.Sales;
using WebAPI.ViewModels.Response.Shops;
using WebAPI.ViewModels.Response.Units;
using WebAPI.ViewModels.Response.Users;

namespace WebAPI.Mapper
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            #region User
            //User
            CreateMap<AppUser, UserResponse>()
                .ForMember(u => u.SecondName, opt => opt.MapFrom(vm => vm.SecondName))
                .ForMember(u => u.Email, opt => opt.MapFrom(vm => vm.Email))
                .ForMember(u => u.Phone, opt => opt.MapFrom(vm => vm.PhoneNumber))
                .ForMember(u => u.Photo, opt => opt.MapFrom(vm => !string.IsNullOrEmpty(vm.Photo) ? string.Concat(ImagePath.RequestUsersImagePath, "/", vm.Photo) : ""));

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

            //Gender
            CreateMap<Gender, GenderResponse>()
                .ForMember(c => c.Name, opt => opt.MapFrom(
                        vm => vm.GenderTranslations.FirstOrDefault(c => c.LanguageId == CurrentLanguage.Id).Name));
            #endregion

            #region Category
            //Category
            CreateMap<Category, CategoryResponse>()
                .ForMember(c => c.Name, opt => opt.MapFrom(
                        vm => vm.CategoryTranslations.FirstOrDefault(c => c.LanguageId == CurrentLanguage.Id).Name))
                .ForMember(u => u.Image, opt => opt.MapFrom(
                    vm => !string.IsNullOrEmpty(vm.Image) ? String.Concat(ImagePath.RequestCategoriesImagePath, "/", vm.Image) : ""))
                .ForMember(u => u.LightIcon, opt => opt.MapFrom(
                        vm => !string.IsNullOrEmpty(vm.LightIcon) ? String.Concat(ImagePath.RequestCategoriesImagePath, "/", vm.LightIcon) : ""))
                .ForMember(u => u.DarkIcon, opt => opt.MapFrom(
                        vm => !string.IsNullOrEmpty(vm.DarkIcon) ? String.Concat(ImagePath.RequestCategoriesImagePath, "/", vm.DarkIcon) : ""))
                .ForMember(u => u.ActiveIcon, opt => opt.MapFrom(
                        vm => !string.IsNullOrEmpty(vm.ActiveIcon) ? String.Concat(ImagePath.RequestCategoriesImagePath, "/", vm.ActiveIcon) : ""))
                .ForMember(c => c.ParentName, opt => opt.MapFrom(
                        vm => vm.Parent.CategoryTranslations.FirstOrDefault(c => c.LanguageId == CurrentLanguage.Id).Name));

            CreateMap<Category, CatalogItemResponse>()
                .ForMember(c => c.Name, opt => opt.MapFrom(
                        vm => vm.CategoryTranslations.FirstOrDefault(c => c.LanguageId == CurrentLanguage.Id).Name))
                .ForMember(u => u.Image, opt => opt.MapFrom(
                        vm => !string.IsNullOrEmpty(vm.Image) ? String.Concat(ImagePath.RequestCategoriesImagePath, "/", vm.Image) : ""));

            CreateMap<Category, FullCatalogItemResponse>()
                .ForMember(c => c.Name, opt => opt.MapFrom(
                        vm => vm.CategoryTranslations.FirstOrDefault(c => c.LanguageId == CurrentLanguage.Id).Name))
                .ForMember(u => u.LightIcon, opt => opt.MapFrom(
                        vm => !string.IsNullOrEmpty(vm.LightIcon) ? String.Concat(ImagePath.RequestCategoriesImagePath, "/", vm.LightIcon) : ""))
                .ForMember(u => u.DarkIcon, opt => opt.MapFrom(
                        vm => !string.IsNullOrEmpty(vm.DarkIcon) ? String.Concat(ImagePath.RequestCategoriesImagePath, "/", vm.DarkIcon) : ""))
                .ForMember(u => u.ActiveIcon, opt => opt.MapFrom(
                        vm => !string.IsNullOrEmpty(vm.ActiveIcon) ? String.Concat(ImagePath.RequestCategoriesImagePath, "/", vm.ActiveIcon) : ""));

            CreateMap<Category, CategoryForSelectResponse>()
                .ForMember(c => c.Name, opt => opt.MapFrom(
                       vm => vm.CategoryTranslations.FirstOrDefault(c => c.LanguageId == CurrentLanguage.Id).Name));

            CreateMap<Category, CategoryFullInfoResponse>()
                .ForMember(c => c.EnglishName, opt => opt.MapFrom(
                        vm => vm.CategoryTranslations.FirstOrDefault(c => c.LanguageId == LanguageId.English).Name))
                .ForMember(c => c.UkrainianName, opt => opt.MapFrom(
                        vm => vm.CategoryTranslations.FirstOrDefault(c => c.LanguageId == LanguageId.Ukrainian).Name))
                .ForMember(u => u.Image, opt => opt.MapFrom(
                        vm => !string.IsNullOrEmpty(vm.Image) ? String.Concat(ImagePath.RequestCategoriesImagePath, "/", vm.Image) : ""))
                .ForMember(u => u.LightIcon, opt => opt.MapFrom(
                        vm => !string.IsNullOrEmpty(vm.LightIcon) ? String.Concat(ImagePath.RequestCategoriesImagePath, "/", vm.LightIcon) : ""))
                .ForMember(u => u.DarkIcon, opt => opt.MapFrom(
                        vm => !string.IsNullOrEmpty(vm.DarkIcon) ? String.Concat(ImagePath.RequestCategoriesImagePath, "/", vm.DarkIcon) : ""))
                .ForMember(u => u.ActiveIcon, opt => opt.MapFrom(
                        vm => !string.IsNullOrEmpty(vm.ActiveIcon) ? String.Concat(ImagePath.RequestCategoriesImagePath, "/", vm.ActiveIcon) : ""))
                .ForMember(c => c.ParentName, opt => opt.MapFrom(
                        vm => vm.Parent.CategoryTranslations.FirstOrDefault(c => c.LanguageId == CurrentLanguage.Id).Name));

            CreateMap<CategoryRequest, Category>()
                .ForMember(c => c.CategoryTranslations, opt => opt.MapFrom(
                       vm => new List<CategoryTranslation>() {
                          new(){LanguageId=LanguageId.English, Name=vm.EnglishName},
                          new(){LanguageId=LanguageId.Ukrainian, Name=vm.UkrainianName}
                    }))
                .ForMember(u => u.Image, opt => opt.Ignore())
                .ForMember(u => u.LightIcon, opt => opt.Ignore())
                .ForMember(u => u.DarkIcon, opt => opt.Ignore())
                .ForMember(u => u.ActiveIcon, opt => opt.Ignore());
            #endregion

            #region Characteristic
            //CharacteristicGroup
            CreateMap<CharacteristicGroupRequest, CharacteristicGroup>();
            CreateMap<CharacteristicGroup, CharacteristicGroupResponse>();

            //CharacteristicName
            CreateMap<CharacteristicNameRequest, CharacteristicName>();
            CreateMap<CharacteristicName, CharacteristicNameResponse>()
                .ForMember(u => u.CharacteristicGroupName, opt => opt.MapFrom(vm => vm.CharacteristicGroup.Name))
             .ForMember(u => u.UnitMeasure, opt => opt.MapFrom(vm => vm.Unit.UnitTranslations.FirstOrDefault(
                        c => c.LanguageId == CurrentLanguage.Id).Measure));

            //CharacteristicValue
            CreateMap<CharacteristicValueRequest, CharacteristicValue>();
            CreateMap<CharacteristicValue, CharacteristicValueResponse>()
                .ForMember(u => u.CharacteristicName, opt => opt.MapFrom(vm => vm.CharacteristicName.Name));
            #endregion

            #region Country
            //Country
            CreateMap<CountryRequest, Country>()
                .ForMember(c => c.CountryTranslations, opt => opt.MapFrom(
                    vm => new List<CountryTranslation>() {
                          new(){LanguageId=LanguageId.English, Name=vm.EnglishName},
                          new(){LanguageId=LanguageId.Ukrainian, Name=vm.UkrainianName}
                    }));
            CreateMap<Country, CountryResponse>()
                .ForMember(c => c.Name, opt => opt.MapFrom(
                       vm => vm.CountryTranslations.FirstOrDefault(c => c.LanguageId == CurrentLanguage.Id).Name));

            CreateMap<Country, CountryFullInfoResponse>()
                .ForMember(c => c.EnglishName, opt => opt.MapFrom(
                       vm => vm.CountryTranslations.FirstOrDefault(c => c.LanguageId == LanguageId.English).Name))
                .ForMember(c => c.UkrainianName, opt => opt.MapFrom(
                       vm => vm.CountryTranslations.FirstOrDefault(c => c.LanguageId == LanguageId.Ukrainian).Name));

            //City
            CreateMap<CityRequest, City>()
                    .ForMember(c => c.CityTranslations, opt => opt.MapFrom(
                       vm => new List<CityTranslation>() {
                          new(){LanguageId=LanguageId.English, Name=vm.EnglishName},
                          new(){LanguageId=LanguageId.Ukrainian, Name=vm.UkrainianName}
                    }));

            CreateMap<City, CityResponse>()
                    .ForMember(c => c.Name, opt => opt.MapFrom(
                       vm => vm.CityTranslations.FirstOrDefault(c => c.LanguageId == CurrentLanguage.Id).Name))
                    .ForMember(u => u.CountryName, opt => opt.MapFrom(
                       vm => vm.Country.CountryTranslations.FirstOrDefault(c => c.LanguageId == CurrentLanguage.Id).Name));

            CreateMap<City, CityForSelectResponse>()
                    .ForMember(c => c.Name, opt => opt.MapFrom(
                       vm => vm.CityTranslations.FirstOrDefault(c => c.LanguageId == CurrentLanguage.Id).Name));

            CreateMap<City, CityFullInfoResponse>()
                .ForMember(c => c.EnglishName, opt => opt.MapFrom(
                       vm => vm.CityTranslations.FirstOrDefault(c => c.LanguageId == LanguageId.English).Name))
                .ForMember(c => c.UkrainianName, opt => opt.MapFrom(
                       vm => vm.CityTranslations.FirstOrDefault(c => c.LanguageId == LanguageId.Ukrainian).Name))
                .ForMember(u => u.CountryName, opt => opt.MapFrom(
                       vm => vm.Country.CountryTranslations.FirstOrDefault(c => c.LanguageId == CurrentLanguage.Id).Name));

            #endregion

            #region Filter
            //FilterGroup
            CreateMap<FilterGroupRequest, FilterGroup>()
                .ForMember(c => c.FilterGroupTranslations, opt => opt.MapFrom(
                       vm => new List<FilterGroupTranslation>() {
                          new(){LanguageId=LanguageId.English, Name=vm.EnglishName},
                          new(){LanguageId=LanguageId.Ukrainian, Name=vm.UkrainianName}
                    }));
            CreateMap<FilterGroup, FilterGroupResponse>()
                .ForMember(c => c.Name, opt => opt.MapFrom(
                       vm => vm.FilterGroupTranslations.FirstOrDefault(c => c.LanguageId == CurrentLanguage.Id).Name));

            CreateMap<FilterGroup, FilterGroupFullInfoResponse>()
                .ForMember(c => c.EnglishName, opt => opt.MapFrom(
                        vm => vm.FilterGroupTranslations.FirstOrDefault(c => c.LanguageId == LanguageId.English).Name))
                .ForMember(c => c.UkrainianName, opt => opt.MapFrom(
                       vm => vm.FilterGroupTranslations.FirstOrDefault(c => c.LanguageId == LanguageId.Ukrainian).Name));

            //FilterName
            CreateMap<FilterNameRequest, FilterName>()
                .ForMember(c => c.FilterNameTranslations, opt => opt.MapFrom(
                       vm => new List<FilterNameTranslation>() {
                          new(){LanguageId=LanguageId.English, Name=vm.EnglishName},
                          new(){LanguageId=LanguageId.Ukrainian, Name=vm.UkrainianName}
                    }));
            CreateMap<FilterName, FilterNameSellerResponse>();
            CreateMap<FilterName, FilterNameResponse>()
                .ForMember(u => u.FilterGroupName, opt => opt.MapFrom(vm => vm.FilterGroup.FilterGroupTranslations.FirstOrDefault(c => c.LanguageId == CurrentLanguage.Id).Name))
                .ForMember(u => u.UnitMeasure, opt => opt.MapFrom(vm => vm.Unit.UnitTranslations.FirstOrDefault(
                           c => c.LanguageId == CurrentLanguage.Id).Measure))
                .ForMember(c => c.Name, opt => opt.MapFrom(
                       vm => vm.FilterNameTranslations.FirstOrDefault(c => c.LanguageId == CurrentLanguage.Id).Name));

            CreateMap<FilterName, FilterNameFullInfoResponse>()
                .ForMember(c => c.EnglishName, opt => opt.MapFrom(
                        vm => vm.FilterNameTranslations.FirstOrDefault(c => c.LanguageId == LanguageId.English).Name))
                .ForMember(c => c.UkrainianName, opt => opt.MapFrom(
                       vm => vm.FilterNameTranslations.FirstOrDefault(c => c.LanguageId == LanguageId.Ukrainian).Name))
                .ForMember(u => u.FilterGroupName, opt => opt.MapFrom(
                        vm => vm.FilterGroup.FilterGroupTranslations.FirstOrDefault(c => c.LanguageId == CurrentLanguage.Id).Name))
                .ForMember(u => u.UnitMeasure, opt => opt.MapFrom(
                        vm => vm.Unit.UnitTranslations.FirstOrDefault(c => c.LanguageId == CurrentLanguage.Id).Measure));


            //FilterValue
            CreateMap<FilterValueRequest, FilterValue>()
                .ForMember(c => c.FilterValueTranslations, opt => opt.MapFrom(
                       vm => new List<FilterValueTranslation>() {
                          new(){LanguageId=LanguageId.English, Value=vm.EnglishValue},
                          new(){LanguageId=LanguageId.Ukrainian, Value=vm.UkrainianValue}
                    }));
            CreateMap<FilterValue, FilterValueCatalogResponse>()
                .ForMember(c => c.Value, opt => opt.MapFrom(
                       vm => vm.FilterValueTranslations.FirstOrDefault(c => c.LanguageId == CurrentLanguage.Id).Value));

            CreateMap<FilterValue, FilterValueSellerResponse>()
                .ForMember(c => c.Value, opt => opt.MapFrom(
                       vm => vm.FilterValueTranslations.FirstOrDefault(c => c.LanguageId == CurrentLanguage.Id).Value));
            CreateMap<FilterValue, FilterValueResponse>()
                .ForMember(c => c.Value, opt => opt.MapFrom(
                       vm => vm.FilterValueTranslations.FirstOrDefault(c => c.LanguageId == CurrentLanguage.Id).Value))
                .ForMember(u => u.FilterName, opt => opt.MapFrom(
                       vm => vm.FilterName.FilterNameTranslations.FirstOrDefault(c => c.LanguageId == CurrentLanguage.Id).Name));

            CreateMap<FilterValue, ProductFilterValue>()
                .ForMember(c => c.Value, opt => opt.MapFrom(
                       vm => vm.FilterValueTranslations.FirstOrDefault(c => c.LanguageId == CurrentLanguage.Id).Value))
                .ForMember(u => u.FilterName, opt => opt.MapFrom(
                       vm => vm.FilterName.FilterNameTranslations.FirstOrDefault(c => c.LanguageId == CurrentLanguage.Id).Name))
                .ForMember(u => u.UnitMeasure, opt => opt.MapFrom(
                       vm => vm.FilterName.Unit.UnitTranslations.FirstOrDefault(c => c.LanguageId == CurrentLanguage.Id).Measure));

            CreateMap<FilterValue, FilterValueFullInfoResponse>()
               .ForMember(c => c.EnglishValue, opt => opt.MapFrom(
                       vm => vm.FilterValueTranslations.FirstOrDefault(c => c.LanguageId == LanguageId.English).Value))
                .ForMember(c => c.UkrainianValue, opt => opt.MapFrom(
                       vm => vm.FilterValueTranslations.FirstOrDefault(c => c.LanguageId == LanguageId.Ukrainian).Value))
                .ForMember(u => u.FilterName, opt => opt.MapFrom(
                       vm => vm.FilterName.FilterNameTranslations.FirstOrDefault(c => c.LanguageId == CurrentLanguage.Id).Name));
            #endregion

            #region Shop
            //Shop
            CreateMap<Shop, ShopResponse>()
                .ForMember(u => u.CountryName, opt => opt.MapFrom(
                    vm => vm.City.Country.CountryTranslations.FirstOrDefault(c => c.LanguageId == CurrentLanguage.Id).Name))
                .ForMember(u => u.CityName, opt => opt.MapFrom(
                    vm => vm.City.CityTranslations.FirstOrDefault(c => c.LanguageId == CurrentLanguage.Id).Name))
                .ForMember(u => u.UserFullName, opt => opt.MapFrom(
                    vm => vm.User.FirstName + " " + vm.User.SecondName))
                .ForMember(u => u.Photo, opt => opt.MapFrom(
                    vm => !string.IsNullOrEmpty(vm.Photo) ? String.Concat(ImagePath.RequestShopsImagePath, "/", vm.Photo) : ""));
            CreateMap<Shop, ShopInfoFromProductResponse>()
                .ForMember(u => u.Adress, opt => opt.MapFrom(vm => vm.City.Country.CountryTranslations.FirstOrDefault(c => c.LanguageId == CurrentLanguage.Id).Name + ", " + vm.City.CityTranslations.FirstOrDefault(c => c.LanguageId == CurrentLanguage.Id).Name))
                .ForMember(u => u.Photo, opt => opt.MapFrom(vm => !string.IsNullOrEmpty(vm.Photo) ? String.Concat(ImagePath.RequestShopsImagePath, "/", vm.Photo) : ""));
            CreateMap<Shop, ShopPageInfoResponse>()
                .ForMember(s => s.CountReviews, opt => opt.MapFrom(vm => vm.ShopReviews.Count))
                .ForMember(s => s.AverageInformationRelevanceRating, opt => opt.MapFrom(vm => vm.ShopReviews.Count > 0 ? Math.Round(vm.ShopReviews.Average(sr => sr.InformationRelevanceRating), 1) : 0))
                .ForMember(s => s.AverageServiceQualityRating, opt => opt.MapFrom(vm => vm.ShopReviews.Count > 0 ? Math.Round(vm.ShopReviews.Average(sr => sr.ServiceQualityRating), 1) : 0))
                .ForMember(s => s.AverageTimelinessRating, opt => opt.MapFrom(vm => vm.ShopReviews.Count > 0 ? Math.Round(vm.ShopReviews.Average(sr => sr.TimelinessRating), 1) : 0))
                .ForMember(s => s.AverageRating, opt => opt.MapFrom(vm => vm.ShopReviews.Count > 0 ?
                                Math.Round(vm.ShopReviews.Average(sr => (sr.InformationRelevanceRating + sr.ServiceQualityRating + sr.TimelinessRating) / 3f), 1) : 0))
                .ForMember(s => s.Schedule, opt => opt.Ignore());

            CreateMap<ShopRequest, Shop>();

            CreateMap<UpdateShopRequest, Shop>()
                .ForMember(s => s.Photo, opt => opt.Ignore());

            CreateMap<ShopPhoneRequest, ShopPhone>();

            //ShopPhone
            CreateMap<ShopPhone, string>()
                .ConstructUsing(u => u.Phone);

            //Shop Review
            CreateMap<ShopReviewRequest, ShopReview>()
                 .ForMember(r => r.Date, opt => opt.MapFrom(o => DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc)));

            CreateMap<ShopReview, ShopReviewResponse>()
                 .ForMember(q => q.Date, opt => opt.MapFrom(vm => vm.Date.ToString("dd MMMM yyyy")));
            #endregion

            #region Product
            //Product
            CreateMap<Product, ProductResponse>()
                .ForMember(u => u.Image, opt => opt.MapFrom(
                    vm => vm.Images.Count != 0 ? Path.Combine(ImagePath.RequestProductsImagePath, vm.Images.FirstOrDefault().Name) : ""))
                .ForMember(u => u.ShopName, opt => opt.MapFrom(vm => vm.Shop.Name))
                .ForMember(u => u.StatusName, opt => opt.MapFrom(
                    vm => vm.Status.ProductStatusTranslations.FirstOrDefault(s => s.LanguageId == CurrentLanguage.Id).Name))
                .ForMember(u => u.CategoryName, opt => opt.MapFrom(
                    vm => vm.Category.CategoryTranslations.FirstOrDefault(c => c.LanguageId == CurrentLanguage.Id).Name));

            CreateMap<Product, ProductPageResponse>()
                .ForMember(u => u.ShopName, opt => opt.MapFrom(vm => vm.Shop.Name))
                .ForMember(u => u.ProductStatus, opt => opt.MapFrom(vm =>
                           vm.Status.ProductStatusTranslations.FirstOrDefault(s => s.LanguageId == CurrentLanguage.Id).Name))
                .ForMember(u => u.Images, opt => opt.MapFrom(vm => vm.Images))
                .ForMember(u => u.Discount, opt => opt.MapFrom(vm => vm.Discount > 0 ?
                vm.Price - (vm.Price / 100f * vm.Discount) : (float?)null))
                .ForMember(u => u.ShopName, opt => opt.MapFrom(vm => vm.Shop.Name))
                .ForMember(u => u.ShopRating, opt => opt.MapFrom(vm => vm.Shop.ShopReviews.Count > 0 ?
                                Math.Round(vm.Shop.ShopReviews.Average(
                                           sr => (sr.InformationRelevanceRating + sr.ServiceQualityRating + sr.TimelinessRating) / 3f), 1) : 0));

            CreateMap<Product, ProductRatingResponse>()
                .ForMember(s => s.CountReviews, opt => opt.MapFrom(vm => vm.Reviews.Count))
                .ForMember(u => u.Rating, opt => opt.MapFrom(vm => vm.Reviews.Count > 0 ?
                                Math.Round(vm.Reviews.Average(sr => sr.ProductRating), 1) : 0));

            CreateMap<Product, ProductCatalogResponse>()
                .ForMember(u => u.StatusName, opt => opt.MapFrom(vm =>
                        vm.Status.ProductStatusTranslations.FirstOrDefault(s => s.LanguageId == CurrentLanguage.Id).Name))
                .ForMember(u => u.Discount, opt => opt.MapFrom(vm => vm.Discount > 0 ?
                vm.Price - (vm.Price / 100f * vm.Discount) : (float?)null))
                .ForMember(u => u.Image, opt => opt.MapFrom(
                        vm => vm.Images.Count != 0 ? Path.Combine(ImagePath.RequestProductsImagePath, vm.Images.FirstOrDefault().Name) : ""));

            CreateMap<ProductCreateRequest, Product>()
                .ForMember(u => u.Images, opt => opt.Ignore());

            CreateMap<Product, ProductWithCartResponse>()
                .ForMember(u => u.StatusName, opt => opt.MapFrom(vm =>
                        vm.Status.ProductStatusTranslations.FirstOrDefault(s => s.LanguageId == CurrentLanguage.Id).Name))
                .ForMember(u => u.Image, opt => opt.MapFrom(
                        vm => vm.Images.Count != 0 ? Path.Combine(ImagePath.RequestProductsImagePath, vm.Images.FirstOrDefault().Name) : ""));


            //ProductStatus
            CreateMap<ProductStatusRequest, ProductStatus>().ForMember(c => c.ProductStatusTranslations, opt => opt.MapFrom(
                    vm => new List<ProductStatusTranslation>() {
                          new(){LanguageId=LanguageId.English, Name=vm.EnglishName},
                          new(){LanguageId=LanguageId.Ukrainian, Name=vm.UkrainianName}
                    }));
            CreateMap<ProductStatus, ProductStatusResponse>().ForMember(c => c.Name, opt => opt.MapFrom(
                       vm => vm.ProductStatusTranslations.FirstOrDefault(c => c.LanguageId == CurrentLanguage.Id).Name));

            CreateMap<ProductStatus, ProductStatusFullInfoResponse>()
                .ForMember(c => c.EnglishName, opt => opt.MapFrom(
                       vm => vm.ProductStatusTranslations.FirstOrDefault(c => c.LanguageId == LanguageId.English).Name))
                .ForMember(c => c.UkrainianName, opt => opt.MapFrom(
                       vm => vm.ProductStatusTranslations.FirstOrDefault(c => c.LanguageId == LanguageId.Ukrainian).Name));

            //ProductImage
            CreateMap<ProductImage, ProductImageResponse>()
                .ForMember(u => u.Name, opt => opt.MapFrom(
                      vm => Path.Combine(ImagePath.RequestProductsImagePath, vm.Name)));
            CreateMap<ProductImage, string>()
                .ConstructUsing(u => Path.Combine(ImagePath.RequestProductsImagePath, u.Name));
            #endregion

            #region Basket
            //BasketItem
            CreateMap<BasketItem, BasketResponse>()
                .ForMember(u => u.ProductName, opt => opt.MapFrom(vm => vm.Product.Name))
                .ForMember(u => u.ProductPrice, opt => opt.MapFrom(vm => vm.Product.Price))
                .ForMember(u => u.ProductCount, opt => opt.MapFrom(vm => vm.Product.Count))
                .ForMember(u => u.ProductUrlSlug, opt => opt.MapFrom(vm => vm.Product.UrlSlug))
                .ForMember(u => u.ProductImage, opt => opt.MapFrom(
                    vm => vm.Product.Images.Count != 0 ? Path.Combine(ImagePath.RequestProductsImagePath, vm.Product.Images.FirstOrDefault().Name) : ""));
            CreateMap<BasketItem, BasketOrderItemResponse>()
                .ForMember(u => u.ProductId, opt => opt.MapFrom(vm => vm.Product.Id))
                .ForMember(u => u.ProductName, opt => opt.MapFrom(vm => vm.Product.Name))
                .ForMember(u => u.ProductPrice, opt => opt.MapFrom(vm => vm.Product.Price))
                .ForMember(u => u.ProductDiscount, opt => opt.MapFrom(
                    vm => vm.Product.Discount > 0 ? vm.Product.Price - (vm.Product.Price / 100f * vm.Product.Discount) : (float?)null))
                .ForMember(u => u.ProductPriceSum, opt => opt.MapFrom(vm => vm.Product.Price * vm.Count))
                .ForMember(u => u.ProductUrlSlug, opt => opt.MapFrom(vm => vm.Product.UrlSlug))
                .ForMember(u => u.ProductImage, opt => opt.MapFrom(
                    vm => vm.Product.Images.Count != 0 ? Path.Combine(ImagePath.RequestProductsImagePath, vm.Product.Images.FirstOrDefault().Name) : ""));
            #endregion

            #region Unit
            //Unit
            CreateMap<UnitRequest, Unit>()
                .ForMember(c => c.UnitTranslations, opt => opt.MapFrom(
                    vm => new List<UnitTranslation>() {
                          new(){LanguageId=LanguageId.English, Measure=vm.EnglishMeasure},
                          new(){LanguageId=LanguageId.Ukrainian, Measure=vm.UkrainianMeasure}
                    }));
            CreateMap<Unit, UnitResponse>()
                .ForMember(c => c.Measure, opt => opt.MapFrom(
                       vm => vm.UnitTranslations.FirstOrDefault(c => c.LanguageId == CurrentLanguage.Id).Measure));

            CreateMap<Unit, UnitFullInfoResponse>()
                .ForMember(c => c.EnglishMeasure, opt => opt.MapFrom(
                       vm => vm.UnitTranslations.FirstOrDefault(c => c.LanguageId == LanguageId.English).Measure))
                .ForMember(c => c.UkrainianMeasure, opt => opt.MapFrom(
                       vm => vm.UnitTranslations.FirstOrDefault(c => c.LanguageId == LanguageId.Ukrainian).Measure));

            #endregion

            #region Order

            //Delivery type
            CreateMap<DeliveryTypeRequest, DeliveryType>()
               .ForMember(c => c.DeliveryTypeTranslations, opt => opt.MapFrom(
                    vm => new List<DeliveryTypeTranslation>() {
                          new(){LanguageId=LanguageId.English, Name=vm.EnglishName},
                          new(){LanguageId=LanguageId.Ukrainian, Name=vm.UkrainianName}
                    }));
            CreateMap<DeliveryType, DeliveryTypeResponse>()
                .ForMember(c => c.Name, opt => opt.MapFrom(
                    vm => vm.DeliveryTypeTranslations.FirstOrDefault(c => c.LanguageId == CurrentLanguage.Id).Name))
                .ForMember(u => u.DarkIcon, opt => opt.MapFrom(
                    vm => !string.IsNullOrEmpty(vm.DarkIcon) ? String.Concat(ImagePath.RequestDeliveryTypesImagePath, "/", vm.DarkIcon) : ""))
                .ForMember(u => u.LightIcon, opt => opt.MapFrom(
                    vm => !string.IsNullOrEmpty(vm.LightIcon) ? String.Concat(ImagePath.RequestDeliveryTypesImagePath, "/", vm.LightIcon) : ""));

            CreateMap<DeliveryType, DeliveryTypeFullInfoResponse>()
                .ForMember(c => c.EnglishName, opt => opt.MapFrom(
                    vm => vm.DeliveryTypeTranslations.FirstOrDefault(c => c.LanguageId == LanguageId.English).Name))
                .ForMember(c => c.UkrainianName, opt => opt.MapFrom(
                    vm => vm.DeliveryTypeTranslations.FirstOrDefault(c => c.LanguageId == LanguageId.Ukrainian).Name))
                .ForMember(u => u.DarkIcon, opt => opt.MapFrom(
                    vm => !string.IsNullOrEmpty(vm.DarkIcon) ? String.Concat(ImagePath.RequestDeliveryTypesImagePath, "/", vm.DarkIcon) : ""))
                .ForMember(u => u.LightIcon, opt => opt.MapFrom(
                    vm => !string.IsNullOrEmpty(vm.LightIcon) ? String.Concat(ImagePath.RequestDeliveryTypesImagePath, "/", vm.LightIcon) : ""));

            //Order status
            CreateMap<OrderStatusRequest, OrderStatus>()
               .ForMember(c => c.OrderStatusTranslations, opt => opt.MapFrom(
                    vm => new List<OrderStatusTranslation>() {
                          new(){LanguageId=LanguageId.English, Name=vm.EnglishName},
                          new(){LanguageId=LanguageId.Ukrainian, Name=vm.UkrainianName}
                    }));
            CreateMap<OrderStatus, OrderStatusResponse>()
                .ForMember(c => c.Name, opt => opt.MapFrom(
                    vm => vm.OrderStatusTranslations.FirstOrDefault(c => c.LanguageId == CurrentLanguage.Id).Name));

            CreateMap<OrderStatus, OrderStatusFullInfoResponse>()
                .ForMember(c => c.EnglishName, opt => opt.MapFrom(
                    vm => vm.OrderStatusTranslations.FirstOrDefault(c => c.LanguageId == LanguageId.English).Name))
                .ForMember(c => c.UkrainianName, opt => opt.MapFrom(
                    vm => vm.OrderStatusTranslations.FirstOrDefault(c => c.LanguageId == LanguageId.Ukrainian).Name));

            //Order
            CreateMap<OrderCreateRequest, Order>()
                .ForMember(o => o.OrderProducts, opt => opt.Ignore());

            CreateMap<Order, OrderResponse>()
                .ForMember(o => o.DeliveryType, opt => opt.MapFrom(
                    vm => vm.DeliveryType.DeliveryTypeTranslations.FirstOrDefault(c => c.LanguageId == CurrentLanguage.Id).Name));

            CreateMap<OrderProduct, OrderProductResponse>()
                .ForMember(r => r.ProductName, opt => opt.MapFrom(o => o.Product.Name))
                .ForMember(r => r.ProductImage, opt => opt.MapFrom(vm => vm.Product.Images.Count != 0 ? Path.Combine(ImagePath.RequestProductsImagePath, vm.Product.Images.FirstOrDefault().Name) : ""));

            #endregion

            #region Review
            //Review
            CreateMap<ReviewRequest, Review>()
                 .ForMember(r => r.Images, opt => opt.Ignore())
                 .ForMember(r => r.Date, opt => opt.MapFrom(o => DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc)));

            CreateMap<Review, ReviewResponse>()
                 .ForMember(r => r.Dislikes, opt => opt.MapFrom(vm => vm.CountDislikes))
                 .ForMember(r => r.Likes, opt => opt.MapFrom(vm => vm.CountLikes))
                 .ForMember(r => r.Replies, opt => opt.MapFrom(vm => vm.Replies.Count))
                 .ForMember(q => q.Date, opt => opt.MapFrom(vm => vm.Date.ToString("dd MMMM yyyy")));

            //ReviewReply
            CreateMap<ReviewReplyRequest, ReviewReply>()
                .ForMember(r => r.Date, opt => opt.MapFrom(o => DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc)));
            CreateMap<ReviewReply, ReviewReplyResponse>()
                .ForMember(q => q.Date, opt => opt.MapFrom(vm => vm.Date.ToString("dd MMMM yyyy")));


            //Review Image
            CreateMap<ReviewImage, string>()
                    .ConstructUsing(r => Path.Combine(ImagePath.RequestReviewsImagePath, r.Name));
            CreateMap<ReviewImage, ReviewImageResponse>()
                    .ForMember(r => r.Name, opt => opt.MapFrom(
                        vm => Path.Combine(ImagePath.RequestReviewsImagePath, vm.Name)));
            #endregion

            #region Question
            //Question
            CreateMap<QuestionRequest, Question>()
                .ForMember(r => r.Images, opt => opt.Ignore())
                .ForMember(r => r.Date, opt => opt.MapFrom(o => DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc)));

            CreateMap<Question, QuestionResponse>()
                .ForMember(r => r.Dislikes, opt => opt.MapFrom(vm => vm.CountDislikes))
                .ForMember(r => r.Likes, opt => opt.MapFrom(vm => vm.CountLikes))
                .ForMember(r => r.Replies, opt => opt.MapFrom(vm => vm.Replies.Count))
                .ForMember(q => q.Date, opt => opt.MapFrom(vm => vm.Date.ToString("dd MMMM yyyy")));

            //QuestionReply
            CreateMap<QuestionReplyRequest, QuestionReply>()
                .ForMember(r => r.Date, opt => opt.MapFrom(o => DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc)));
            CreateMap<QuestionReply, QuestionReplyResponse>()
                .ForMember(q => q.Date, opt => opt.MapFrom(vm => vm.Date.ToString("dd MMMM yyyy")));

            //Question Image
            CreateMap<QuestionImage, string>()
                    .ConstructUsing(r => Path.Combine(ImagePath.RequestQuestionsImagePath, r.Name));
            CreateMap<QuestionImage, QuestionImageResponse>()
                    .ForMember(r => r.Name, opt => opt.MapFrom(
                        vm => Path.Combine(ImagePath.RequestQuestionsImagePath, vm.Name)));
            #endregion

            #region Sale

            CreateMap<SaleRequest, Sale>()
                .ForMember(u => u.Categories, opt => opt.Ignore());

            CreateMap<Sale, SaleResponse>()
                .ForMember(u => u.HorizontalImage, opt => opt.MapFrom(
                        vm => !string.IsNullOrEmpty(vm.SaleTranslations.FirstOrDefault(c => c.LanguageId == CurrentLanguage.Id).HorizontalImage) ?
                        string.Concat(ImagePath.RequestSalesImagePath, "/", vm.SaleTranslations.FirstOrDefault(c => c.LanguageId == CurrentLanguage.Id).HorizontalImage) : ""))
                .ForMember(u => u.VerticalImage, opt => opt.MapFrom(
                        vm => !string.IsNullOrEmpty(vm.SaleTranslations.FirstOrDefault(c => c.LanguageId == CurrentLanguage.Id).VerticalImage) ?
                        string.Concat(ImagePath.RequestSalesImagePath, "/", vm.SaleTranslations.FirstOrDefault(c => c.LanguageId == CurrentLanguage.Id).VerticalImage) : ""))
                .ForMember(u => u.DateStart, opt => opt.MapFrom(vm => vm.DateStart.ToString("dd.MM.yyyy HH:mm")))
                .ForMember(u => u.DateEnd, opt => opt.MapFrom(vm => vm.DateEnd.ToString("dd.MM.yyyy HH:mm")));

            CreateMap<Sale, SaleFullInfoResponse>()
               .ForMember(u => u.UkrainianHorizontalImage, opt => opt.MapFrom(
                       vm => !string.IsNullOrEmpty(vm.SaleTranslations.FirstOrDefault(c => c.LanguageId == LanguageId.Ukrainian).HorizontalImage) ?
                       string.Concat(ImagePath.RequestSalesImagePath, "/", vm.SaleTranslations.FirstOrDefault(c => c.LanguageId == LanguageId.Ukrainian).HorizontalImage) : ""))
               .ForMember(u => u.UkrainianVerticalImage, opt => opt.MapFrom(
                       vm => !string.IsNullOrEmpty(vm.SaleTranslations.FirstOrDefault(c => c.LanguageId == LanguageId.Ukrainian).VerticalImage) ?
                       string.Concat(ImagePath.RequestSalesImagePath, "/", vm.SaleTranslations.FirstOrDefault(c => c.LanguageId == LanguageId.Ukrainian).VerticalImage) : ""))
               .ForMember(u => u.EnglishHorizontalImage, opt => opt.MapFrom(
                       vm => !string.IsNullOrEmpty(vm.SaleTranslations.FirstOrDefault(c => c.LanguageId == LanguageId.English).HorizontalImage) ?
                       string.Concat(ImagePath.RequestSalesImagePath, "/", vm.SaleTranslations.FirstOrDefault(c => c.LanguageId == LanguageId.English).HorizontalImage) : ""))
               .ForMember(u => u.EnglishVerticalImage, opt => opt.MapFrom(
                       vm => !string.IsNullOrEmpty(vm.SaleTranslations.FirstOrDefault(c => c.LanguageId == LanguageId.English).VerticalImage) ?
                       string.Concat(ImagePath.RequestSalesImagePath, "/", vm.SaleTranslations.FirstOrDefault(c => c.LanguageId == LanguageId.English).VerticalImage) : ""));
            #endregion
        }
    }
}
