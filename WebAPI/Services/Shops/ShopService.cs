using AutoMapper;
using DAL;
using DAL.Constants;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using System.Net;
using WebAPI.Constants;
using WebAPI.Exceptions;
using WebAPI.Extensions;
using WebAPI.Interfaces.Shops;
using WebAPI.Interfaces.Users;
using WebAPI.Specifications.Shops;
using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Request.Shops;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Shops;
using WebAPI.ViewModels.Response.Users;

namespace WebAPI.Services.Shops
{
    public class ShopService : IShopService
    {
        private readonly IStringLocalizer<ErrorMessages> _errorMessagesLocalizer;
        private readonly IRepository<Shop> _shopRepository;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public ShopService(IStringLocalizer<ErrorMessages> errorMessagesLocalizer,
            IMapper mapper,
            IRepository<Shop> shopRepository,
            IJwtTokenService jwtTokenService,
            UserManager<AppUser> userManager)
        {
            _errorMessagesLocalizer = errorMessagesLocalizer;
            _shopRepository = shopRepository;
            _jwtTokenService = jwtTokenService;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ShopResponse>> GetShopsAsync()
        {
            var spec = new ShopIncludeCityWithCountrySpecification();
            var shops = await _shopRepository.ListAsync(spec);

            var response = _mapper.Map<IEnumerable<ShopResponse>>(shops);
            return response;
        }
        public async Task<ShopResponse> GetShopByIdAsync(int shopId)
        {
            var spec = new ShopIncludeCityWithCountrySpecification(shopId);
            var shop = await _shopRepository.GetBySpecAsync(spec);
            shop.ShopNullChecking();

            var response = _mapper.Map<ShopResponse>(shop);
            return response;
        }

        public async Task<ShopInfoFromProductResponse> ShopInfoFromProductAsync(int shopId)
        {
            var spec = new ShopInfoFromProductSpecification(shopId);
            var shop = await _shopRepository.GetBySpecAsync(spec);
            shop.ShopNullChecking();

            var response = _mapper.Map<ShopInfoFromProductResponse>(shop);
            return response;
        }

        public async Task<AuthResponse> CreateShopAsync(ShopRequest request, string userId, string ipAddress)
        {
            var user = await _userManager.FindByIdAsync(userId);
            user.UserNullChecking();

            var resultPasswordCheck = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!resultPasswordCheck)
                throw new AppException(_errorMessagesLocalizer["InvalidUserData"], HttpStatusCode.Unauthorized);

            var resultRoles = await _userManager.GetRolesAsync(user);
            if (resultRoles.Count == 0)
            {
                var resultRole = await _userManager.AddToRoleAsync(user, Roles.Seller);
                if (!resultRole.Succeeded)
                    throw new AppException(_errorMessagesLocalizer["UserAddRoleFail"]);
            }

            var shop = _mapper.Map<Shop>(request);
            shop.UserId = userId;

            await _shopRepository.AddAsync(shop);
            await _shopRepository.SaveChangesAsync();

            user.ShopId = shop.Id;
            await _userManager.UpdateAsync(user);

            var newRefreshToken = _jwtTokenService.GenerateRefreshToken(ipAddress);
            await _jwtTokenService.SaveRefreshToken(newRefreshToken, user);

            await _jwtTokenService.RemoveOldRefreshTokens(user);

            var response = new AuthResponse
            {
                AccessToken = await _jwtTokenService.GenerateJwtToken(user),
                RefreshToken = newRefreshToken.Token
            };
            return response;
        }

        public async Task UpdateShopAsync(int shopId, ShopRequest request, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            user.UserNullChecking();

            var shop = await _shopRepository.GetByIdAsync(shopId);
            shop.ShopNullChecking();

            //if (!string.IsNullOrEmpty(request.Photo))
            //{
            //    var filePath = Path.Combine(
            //        Directory.GetCurrentDirectory(),
            //        request.Photo.Replace(ImagePath.RequestShopsImagePath,
            //        ImagePath.ShopsImagePath));
            //    if (!File.Exists(filePath))
            //    {
            //        if (!string.IsNullOrEmpty(shop.Photo))
            //        {
            //            filePath = Path.Combine(
            //               Directory.GetCurrentDirectory(),
            //               ImagePath.ShopsImagePath,
            //               shop.Photo);

            //            if (File.Exists(filePath))
            //                File.Delete(filePath);
            //        }
            //        var img = ImageWorker.FromBase64StringToImage(request.Photo);
            //        string randomFilename = Guid.NewGuid() + ".png";
            //        var dir = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.ShopsImagePath, randomFilename);
            //        img.Save(dir, ImageFormat.Png);

            //        shop.Photo = randomFilename;
            //    }
            //}

            _mapper.Map(request, shop);

            await _shopRepository.UpdateAsync(shop);
            await _shopRepository.SaveChangesAsync();
        }

        public async Task DeleteShopAsync(int shopId)
        {
            var shop = await _shopRepository.GetByIdAsync(shopId);
            shop.ShopNullChecking();

            if (!string.IsNullOrEmpty(shop.Photo))
            {
                var filePath = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           ImagePath.ShopsImagePath,
                           shop.Photo);

                if (File.Exists(filePath))
                    File.Delete(filePath);
            }

            await _shopRepository.DeleteAsync(shop);
            await _shopRepository.SaveChangesAsync();
        }

        public async Task<AdminSearchResponse<ShopResponse>> SearchShopsAsync(AdminSearchRequest request)
        {
            var spec = new ShopSearchSpecification(request.Name, request.IsAscOrder, request.OrderBy);
            var count = await _shopRepository.CountAsync(spec);
            spec = new ShopSearchSpecification(
                request.Name,
                request.IsAscOrder,
                request.OrderBy,
                (request.Page - 1) * request.RowsPerPage,
                request.RowsPerPage);
            var shops = await _shopRepository.ListAsync(spec);
            var mappedShops = _mapper.Map<IEnumerable<ShopResponse>>(shops);
            var response = new AdminSearchResponse<ShopResponse>() { Count = count, Values = mappedShops };

            return response;

        }

        public async Task DeleteShopsAsync(IEnumerable<int> ids)
        {
            foreach (var item in ids)
            {
                var shop = await _shopRepository.GetByIdAsync(item);
                if (!string.IsNullOrEmpty(shop.Photo))
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.ShopsImagePath, shop.Photo);

                    if (File.Exists(filePath))
                    {
                        if (File.Exists(filePath))
                        {
                            File.Delete(filePath);
                        }
                    }
                }
                await _shopRepository.DeleteAsync(shop);
            }
            await _shopRepository.SaveChangesAsync();
        }

        public async Task<ShopPageInfoResponse> GetShopInfoAsync(int shopId)
        {
            var spec = new ShopGetWithReviewsSpecification(shopId);

            var shop = await _shopRepository.GetBySpecAsync(spec);
            shop.ShopNullChecking();

            var result = _mapper.Map<ShopPageInfoResponse>(shop);

            var ratings = new List<Rating>();

            for (int i = 0; i < 5; i++)
            {
                ratings.Add(new Rating() { Number = i + 1 });

                if (i == 0)
                    ratings[i].Count = shop.ShopReviews.Count(s =>
                                       ((s.ServiceQualityRating + s.TimelinessRating + s.InformationRelevanceRating) / 3f) <= ratings[i].Number);
                else
                    ratings[i].Count = shop.ShopReviews.Count(s =>
                                   ((s.ServiceQualityRating + s.TimelinessRating + s.InformationRelevanceRating) / 3f) <= ratings[i].Number &&
                                   ((s.ServiceQualityRating + s.TimelinessRating + s.InformationRelevanceRating) / 3f) > ratings[i - 1].Number);
            }
            ratings.Reverse();
            result.Ratings = ratings;

            return result;
        }
    }
}
