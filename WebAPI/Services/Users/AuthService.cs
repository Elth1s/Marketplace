using AutoMapper;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using System.Net;
using WebAPI.Constants;
using WebAPI.Exceptions;
using WebAPI.Extensions;
using WebAPI.Helpers;
using WebAPI.Interfaces.Users;
using WebAPI.ViewModels.Request.Users;
using WebAPI.ViewModels.Response.Users;

namespace WebAPI.Services.Users
{
    public class AuthService : IAuthService
    {
        private readonly IStringLocalizer<ErrorMessages> _errorMessagesLocalizer;
        private readonly IMapper _mapper;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IRecaptchaService _recaptchaService;
        private readonly UserManager<AppUser> _userManager;
        private readonly PhoneNumberManager _phoneNumberManager;
        public AuthService(IStringLocalizer<ErrorMessages> errorMessagesLocalizer,
                           IMapper mapper,
                           IJwtTokenService jwtTokenService,
                           IRecaptchaService recaptchaService,
                           UserManager<AppUser> userManager,
                           PhoneNumberManager phoneNumberManager)
        {
            _errorMessagesLocalizer = errorMessagesLocalizer;
            _mapper = mapper;
            _jwtTokenService = jwtTokenService;
            _recaptchaService = recaptchaService;
            _userManager = userManager;
            _phoneNumberManager = phoneNumberManager;
        }

        public async Task<AuthResponse> SignInAsync(SignInRequest request, string ipAddress)
        {
            if (!_recaptchaService.IsValid(request.ReCaptchaToken))
                throw new AppException(_errorMessagesLocalizer["CaptchaVerificationFailed"]);


            var user = await _userManager.GetByEmailAsync(request.EmailOrPhone);
            if (user == null)
                user = await _userManager.GetByPhoneNumberAsync(_phoneNumberManager.GetPhoneE164Format(request.EmailOrPhone));


            var resultPasswordCheck = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!resultPasswordCheck)
                throw new AppException(_errorMessagesLocalizer["InvalidUserData"], HttpStatusCode.Unauthorized);

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
        public async Task<AuthResponse> SignUpAsync(SignUpRequest request, string ipAddress)
        {
            if (!_recaptchaService.IsValid(request.ReCaptchaToken))
                throw new AppException(_errorMessagesLocalizer["CaptchaVerificationFailed"]);

            var user = _mapper.Map<AppUser>(request);

            if (EmailManager.IsValidEmail(request.EmailOrPhone))
                user.Email = request.EmailOrPhone;
            else
                user.PhoneNumber = _phoneNumberManager.GetPhoneE164Format(request.EmailOrPhone);


            user.UserName = user.Id;

            var resultCreate = await _userManager.CreateAsync(user, request.Password);
            if (!resultCreate.Succeeded)
                throw new AppException(_errorMessagesLocalizer["UserCreateFail"]);


            var refreshToken = _jwtTokenService.GenerateRefreshToken(ipAddress);
            await _jwtTokenService.SaveRefreshToken(refreshToken, user);

            var response = new AuthResponse
            {
                AccessToken = await _jwtTokenService.GenerateJwtToken(user),
                RefreshToken = refreshToken.Token
            };

            return response;
        }

        public async Task<AuthResponse> RefreshTokenAsync(TokenRequest request, string ipAddress)
        {
            var user = _jwtTokenService.GetUserByRefreshToken(request.Token);
            var refreshToken = user.RefreshTokens.Single(x => x.Token == request.Token);
            if (refreshToken.IsRevoked)
            {
                RevokeDescendantRefreshTokens(
                    refreshToken,
                    user,
                    ipAddress, $"Attempted reuse of revoked ancestor token: {request.Token}");
                await _userManager.UpdateAsync(user);
            }
            refreshToken.RefreshTokenNotActiveChecking();

            var newRefreshToken = RotateRefreshToken(refreshToken, ipAddress);
            await _jwtTokenService.SaveRefreshToken(newRefreshToken, user);

            await _jwtTokenService.RemoveOldRefreshTokens(user);

            var response = new AuthResponse
            {
                AccessToken = await _jwtTokenService.GenerateJwtToken(user),
                RefreshToken = newRefreshToken.Token
            };

            return response;
        }

        public async Task RevokeTokenAsync(TokenRequest request, string ipAddress)
        {
            var user = _jwtTokenService.GetUserByRefreshToken(request.Token);
            var refreshToken = user.RefreshTokens.Single(x => x.Token == request.Token);

            refreshToken.RefreshTokenNotActiveChecking();

            refreshToken.RefreshTokenRevokedChecking();

            RevokeRefreshToken(refreshToken, ipAddress);
            await _userManager.UpdateAsync(user);
        }

        public async Task<AuthResponse> GoogleExternalLoginAsync(ExternalLoginRequest request, string ipAddress)
        {
            var payload = await _jwtTokenService.VerifyGoogleToken(request);
            if (payload == null)
                throw new AppException(_errorMessagesLocalizer["InvalidExternalLoginRequest"]);


            var info = new UserLoginInfo(ExternalLoginProviderName.Google, payload.Subject, ExternalLoginProviderName.Google);

            var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

            if (user == null)
            {
                user = await _userManager.GetByEmailAsync(payload.Email);

                if (user == null)
                {
                    user = _mapper.Map<AppUser>(payload);

                    user.UserName = user.Id;

                    var resultCreate = await _userManager.CreateAsync(user);
                    if (!resultCreate.Succeeded)
                        throw new AppException(_errorMessagesLocalizer["UserCreateFail"]);
                }

                var resultAddLogin = await _userManager.AddLoginAsync(user, info);
                if (!resultAddLogin.Succeeded)
                    throw new AppException(_errorMessagesLocalizer["ExternalLoginAddFail"]);
            }

            var refreshToken = _jwtTokenService.GenerateRefreshToken(ipAddress);
            await _jwtTokenService.SaveRefreshToken(refreshToken, user);

            var response = new AuthResponse
            {
                AccessToken = await _jwtTokenService.GenerateJwtToken(user),
                RefreshToken = refreshToken.Token
            };

            return response;
        }

        public async Task<AuthResponse> FacebookExternalLoginAsync(ExternalLoginRequest request, string ipAddress)
        {
            var payload = await _jwtTokenService.VerifyFacebookToken(request);
            if (payload == null)
                throw new AppException(_errorMessagesLocalizer["InvalidExternalLoginRequest"]);

            var info = new UserLoginInfo(ExternalLoginProviderName.Facebook, payload.Id, ExternalLoginProviderName.Facebook);

            var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

            if (user == null)
            {
                user = await _userManager.GetByEmailAsync(payload.Email);

                if (user == null)
                {

                    user = _mapper.Map<AppUser>(payload);

                    user.UserName = user.Id;

                    var resultCreate = await _userManager.CreateAsync(user);
                    if (!resultCreate.Succeeded)
                        throw new AppException(_errorMessagesLocalizer["UserCreateFail"]);
                }

                var resultAddLogin = await _userManager.AddLoginAsync(user, info);
                if (!resultAddLogin.Succeeded)
                    throw new AppException(_errorMessagesLocalizer["ExternalLoginAddFail"]);
            }

            var refreshToken = _jwtTokenService.GenerateRefreshToken(ipAddress);
            await _jwtTokenService.SaveRefreshToken(refreshToken, user);

            var response = new AuthResponse
            {
                AccessToken = await _jwtTokenService.GenerateJwtToken(user),
                RefreshToken = refreshToken.Token
            };

            return response;
        }

        private RefreshToken RotateRefreshToken(RefreshToken refreshToken, string ipAddress)
        {
            var newRefreshToken = _jwtTokenService.GenerateRefreshToken(ipAddress);
            RevokeRefreshToken(refreshToken, ipAddress, "Replaced by new token", newRefreshToken.Token);
            return newRefreshToken;
        }


        private void RevokeRefreshToken(RefreshToken token, string ipAddress, string reason = null, string replacedByToken = null)
        {
            token.Revoked = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
            token.RevokedByIp = ipAddress;
            token.ReasonRevoked = reason;
            token.ReplacedByToken = replacedByToken;
        }

        private void RevokeDescendantRefreshTokens(RefreshToken refreshToken, AppUser user, string ipAddress, string reason)
        {
            if (!string.IsNullOrEmpty(refreshToken.ReplacedByToken))
            {
                var childToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken.ReplacedByToken);
                if (childToken.IsActive)
                    RevokeRefreshToken(childToken, ipAddress, reason);
                else
                    RevokeDescendantRefreshTokens(childToken, user, ipAddress, reason);
            }
        }

    }
}
