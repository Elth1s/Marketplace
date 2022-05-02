using AutoMapper;
using DAL.Entities;
using DAL.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using WebAPI.Exceptions;
using WebAPI.Intefaces;
using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Response;

namespace WebAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IMapper _mapper;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly UserManager<AppUser> _userManager;
        public AuthService(IMapper mapper, IJwtTokenService jwtTokenService, UserManager<AppUser> userManager)
        {
            _mapper = mapper;
            _jwtTokenService = jwtTokenService;
            _userManager = userManager;
        }

        public async Task<AuthResponse> SignInAsync(SignInRequest request, string ipAddress)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            var resultPasswordCheck = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!resultPasswordCheck)
            {
                throw new AppException("Invalid email or password!");
            }

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
            var user = _mapper.Map<AppUser>(request);

            var resultCreate = await _userManager.CreateAsync(user, request.Password);
            if (!resultCreate.Succeeded)
            {
                throw new AppException("User creation failed!");
            }
            //var resultRole = await _userManager.AddToRoleAsync(_user, Roles.User);
            //if (!resultRole.Succeeded)
            //{
            //    throw new AppException($"Failed to add { Roles.User } role to { _user.Email}.");
            //}
            var refreshToken = _jwtTokenService.GenerateRefreshToken(ipAddress);
            await _jwtTokenService.SaveRefreshToken(refreshToken, user);

            var response = new AuthResponse
            {
                AccessToken = await _jwtTokenService.GenerateJwtToken(user),
                RefreshToken = refreshToken.Token
            };

            return response;
        }

        public async Task<AuthResponse> RefreshAccessTokenAsync(TokenRequest request, string ipAddress)
        {
            var user = _jwtTokenService.GetUserByRefreshToken(request.Token);
            var refreshToken = user.RefreshTokens.Single(x => x.Token == request.Token);
            if (refreshToken.IsRevoked)
            {
                RevokeDescendantRefreshTokens(refreshToken, user, ipAddress, $"Attempted reuse of revoked ancestor token: {request.Token}");
                await _userManager.UpdateAsync(user);
            }
            if (!refreshToken.IsActive)
            {
                throw new AppException("Ivalid token.");
            }
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

        public async Task RevokeToken(TokenRequest request, string ipAddress)
        {
            var user = _jwtTokenService.GetUserByRefreshToken(request.Token);
            var refreshToken = user.RefreshTokens.Single(x => x.Token == request.Token);
            if (refreshToken.IsRevoked)
            {
                throw new AppException("Token already revoked");
            }
            if (!refreshToken.IsActive)
            {
                throw new AppException("Ivalid token.");
            }
            RevokeRefreshToken(refreshToken, ipAddress);
            await _userManager.UpdateAsync(user);
        }

        private RefreshToken RotateRefreshToken(RefreshToken refreshToken, string ipAddress)
        {
            var newRefreshToken = _jwtTokenService.GenerateRefreshToken(ipAddress);
            RevokeRefreshToken(refreshToken, ipAddress, "Replaced by new token", newRefreshToken.Token);
            return newRefreshToken;
        }


        private void RevokeRefreshToken(RefreshToken token, string ipAddress, string reason = null, string replacedByToken = null)
        {
            token.Revoked = DateTime.UtcNow;
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
