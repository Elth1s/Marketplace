using AutoMapper;
using DAL;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Drawing.Imaging;
using WebAPI.Constants;
using WebAPI.Exceptions;
using WebAPI.Extensions;
using WebAPI.Helpers;
using WebAPI.Interfaces.Users;
using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Request.Users;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Users;

namespace WebAPI.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IStringLocalizer<ErrorMessages> _errorMessagesLocalizer;
        private readonly IMapper _mapper;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly UserManager<AppUser> _userManager;
        private readonly PhoneNumberManager _phoneNumberManager;
        private readonly IRepository<Country> _countryRepository;
        private readonly IRepository<City> _cityRepository;
        private readonly IRepository<Gender> _genderRepository;
        public UserService(
            IStringLocalizer<ErrorMessages> errorMessagesLocalizer,
            IMapper mapper,
            UserManager<AppUser> userManager,
            IJwtTokenService jwtTokenService,
            PhoneNumberManager phoneNumberManager,
            IRepository<Country> countryRepository,
            IRepository<City> cityRepository,
            IRepository<Gender> genderRepository)
        {
            _errorMessagesLocalizer = errorMessagesLocalizer;
            _mapper = mapper;
            _userManager = userManager;
            _jwtTokenService = jwtTokenService;
            _phoneNumberManager = phoneNumberManager;
            _countryRepository = countryRepository;
            _cityRepository = cityRepository;
            _genderRepository = genderRepository;
        }

        public async Task<AuthResponse> UpdateProfileAsync(string id, UpdateProfileRequest request, string ipAddress)
        {
            var user = await _userManager.FindByIdAsync(id);
            user.UserNullChecking();

            if (request.CountryId.HasValue)
            {
                var country = await _countryRepository.GetByIdAsync(request.CountryId);
                country.CountryNullChecking();
            }
            if (request.CityId.HasValue)
            {
                var city = await _cityRepository.GetByIdAsync(request.CityId);
                city.CityNullChecking();
            }

            if (request.GenderId.HasValue)
            {
                var gender = await _genderRepository.GetByIdAsync(request.GenderId);
                gender.GenderNullChecking();
            }

            if (!string.IsNullOrEmpty(request.Photo))
            {
                var filePath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    request.Photo.Replace(ImagePath.RequestUsersImagePath,
                    ImagePath.UsersImagePath));
                if (!File.Exists(filePath))
                {
                    if (!string.IsNullOrEmpty(user.Photo))
                    {
                        filePath = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           ImagePath.UsersImagePath,
                           user.Photo);

                        if (File.Exists(filePath))
                            File.Delete(filePath);
                    }
                    var img = request.Photo.FromBase64StringToImage();
                    string randomFilename = Guid.NewGuid() + ".png";
                    var dir = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.UsersImagePath, randomFilename);
                    img.Save(dir, ImageFormat.Png);

                    user.Photo = randomFilename;
                }
            }

            _mapper.Map(request, user);

            await _userManager.UpdateAsync(user);

            await _userManager.UpdateNormalizedUserNameAsync(user);

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

        public async Task<ProfileResponse> GetProfileAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            user.UserNullChecking();

            var response = _mapper.Map<ProfileResponse>(user);
            response.HasPassword = await _userManager.HasPasswordAsync(user);

            var userLogins = await _userManager.GetLoginsAsync(user);
            response.IsGoogleConnected = userLogins.FirstOrDefault(u => u.LoginProvider == ExternalLoginProviderName.Google) != null;
            response.IsFacebookConnected = userLogins.FirstOrDefault(u => u.LoginProvider == ExternalLoginProviderName.Facebook) != null;

            return response;
        }

        public async Task ChangeEmailAsync(string userId, ChangeEmailRequest request)
        {
            var user = await _userManager.FindByIdAsync(userId);
            user.UserNullChecking();

            var passwordCheck = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!passwordCheck)
                throw new AppException(_errorMessagesLocalizer["InvalidPassword"]);

            user.Email = request.Email;

            await _userManager.UpdateAsync(user);
        }

        public async Task ChangePhoneAsync(string userId, ChangePhoneRequest request)
        {
            var user = await _userManager.FindByIdAsync(userId);
            user.UserNullChecking();

            var passwordCheck = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!passwordCheck)
                throw new AppException(_errorMessagesLocalizer["InvalidPassword"]);

            user.PhoneNumber = _phoneNumberManager.GetPhoneE164Format(request.Phone);

            await _userManager.UpdateAsync(user);
        }

        public async Task<bool> HasPassword(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            user.UserNullChecking();

            return await _userManager.HasPasswordAsync(user);
        }

        public async Task AddPasswordAsync(string userId, AddPasswordRequest request)
        {
            var user = await _userManager.FindByIdAsync(userId);
            user.UserNullChecking();

            var hasPassword = await _userManager.HasPasswordAsync(user);

            if (hasPassword)
                throw new AppException(_errorMessagesLocalizer["PasswordExist"]);

            var resultPasswordAdd = await _userManager.AddPasswordAsync(user, request.Password);
            if (!resultPasswordAdd.Succeeded)
                throw new AppException(_errorMessagesLocalizer["AddPasswordFailed"]);
        }

        public async Task ChangePasswordAsync(string userId, ChangePasswordRequest request)
        {
            var user = await _userManager.FindByIdAsync(userId);
            user.UserNullChecking();

            var resultPasswordCheck = await _userManager.CheckPasswordAsync(user, request.OldPassword);
            if (!resultPasswordCheck)
                throw new AppException(_errorMessagesLocalizer["InvalidPassword"]);

            var resultPasswordUpdate = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.Password);
            if (!resultPasswordUpdate.Succeeded)
                throw new AppException(_errorMessagesLocalizer["PasswordUpdateFail"]);
        }

        public async Task<SearchResponse<UserResponse>> SearchUsersAsync(AdminSearchRequest request)
        {
            var count = await _userManager.UserSearch(request.Name, request.IsAscOrder, request.OrderBy).CountAsync();
            var users = await _userManager.UserSearch(
                request.Name,
                request.IsAscOrder,
                request.OrderBy,
                (request.Page - 1) * request.RowsPerPage,
                request.RowsPerPage).ToListAsync();

            var mappedUsers = _mapper.Map<IEnumerable<UserResponse>>(users);
            var response = new SearchResponse<UserResponse>() { Count = count, Values = mappedUsers };

            return response;
        }

        public async Task DeleteUsersAsync(IEnumerable<string> ids)
        {
            foreach (var item in ids)
            {
                var user = await _userManager.GetByIdWithIncludeInfo(item);
                user.UserNullChecking();
                var logins = await _userManager.GetLoginsAsync(user);
                if (logins.Any())
                {
                    foreach (var login in logins)
                    {
                        await _userManager.RemoveLoginAsync(user, login.LoginProvider, login.ProviderKey);
                    }
                }

                user.Shop.IsDeleted = true;
                user.IsDeleted = true;
                user.BasketItems = null;
                user.RefreshTokens = null;
                user.SelectedProducts = null;
                user.ReviewedProducts = null;
                user.ComparisonProducts = null;

                await _userManager.UpdateAsync(user);
            }
        }

        public async Task GoogleConnectAsync(ExternalLoginRequest request, string userId)
        {
            var payload = await _jwtTokenService.VerifyGoogleToken(request);
            if (payload == null)
                throw new AppException(_errorMessagesLocalizer["InvalidExternalLoginRequest"]);

            var info = new UserLoginInfo(ExternalLoginProviderName.Google, payload.Subject, ExternalLoginProviderName.Google);

            var user = await _userManager.FindByIdAsync(userId);
            user.UserNullChecking();

            var login = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
            if (login != null)
                throw new AppException(_errorMessagesLocalizer["GoogleAccountAlreadyConnected"]);

            var userLogins = await _userManager.GetLoginsAsync(user);

            var userLoginExist = userLogins.FirstOrDefault(u => u.LoginProvider == info.LoginProvider);
            if (userLoginExist != null)
                throw new AppException(_errorMessagesLocalizer["GoogleConnected"]);

            var resultAddLogin = await _userManager.AddLoginAsync(user, info);
            if (!resultAddLogin.Succeeded)
                throw new AppException(_errorMessagesLocalizer["ExternalLoginAddFail"]);
        }

        public async Task FacebookConnectAsync(ExternalLoginRequest request, string userId)
        {
            var payload = await _jwtTokenService.VerifyFacebookToken(request);
            if (payload == null)
                throw new AppException(_errorMessagesLocalizer["InvalidExternalLoginRequest"]);

            var info = new UserLoginInfo(ExternalLoginProviderName.Facebook, payload.Id, ExternalLoginProviderName.Facebook);

            var user = await _userManager.FindByIdAsync(userId);
            user.UserNullChecking();

            var login = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
            if (login != null)
                throw new AppException(_errorMessagesLocalizer["FacebookAccountAlreadyConnectedUser"]);

            var userLogins = await _userManager.GetLoginsAsync(user);

            var userLoginExist = userLogins.FirstOrDefault(u => u.LoginProvider == info.LoginProvider);
            if (userLoginExist != null)
                throw new AppException(_errorMessagesLocalizer["FacebookConnected"]);

            var resultAddLogin = await _userManager.AddLoginAsync(user, info);
            if (!resultAddLogin.Succeeded)
                throw new AppException(_errorMessagesLocalizer["ExternalLoginAddFail"]);
        }

        public async Task<IEnumerable<UserReviewResponse>> GetUserReviewsAsync(string userId)
        {
            var user = await _userManager.GetUserReviewsAsync(userId);
            user.UserNullChecking();

            var reviews = user.Reviews.OrderByDescending(r => r.Date);
            var products = user.Orders.SelectMany(o => o.OrderProducts).GroupBy(o => o.ProductId);

            var result = new List<UserReviewResponse>();

            foreach (var item in products)
            {
                var product = item.FirstOrDefault().Product;
                var review = reviews.FirstOrDefault(r => r.ProductId == item.Key);
                var response = new UserReviewResponse()
                {
                    ProductName = product.Name,
                    ProductSlug = product.UrlSlug.ToString(),
                    ProductImage = product.Images.Count != 0 ? Path.Combine(ImagePath.RequestProductsImagePath, product.Images.FirstOrDefault().Name) : "",
                    HasReview = review != null,
                    Review = review == null ? "" : review.Comment
                };
                result.Add(response);
            }

            return result;
        }

        public async Task RemoveProfileAsync(string userId)
        {
            var user = await _userManager.GetByIdWithIncludeInfo(userId);
            user.UserNullChecking();
            var logins = await _userManager.GetLoginsAsync(user);
            if (logins.Any())
            {
                foreach (var item in logins)
                {
                    await _userManager.RemoveLoginAsync(user, item.LoginProvider, item.ProviderKey);
                }
            }
            if (user.Shop != null)
                user.Shop.IsDeleted = true;

            user.IsDeleted = true;
            user.BasketItems = null;
            user.RefreshTokens = null;
            user.SelectedProducts = null;
            user.ReviewedProducts = null;
            user.ComparisonProducts = null;

            await _userManager.UpdateAsync(user);
        }
    }
}
