using DAL.Entities;
using DAL.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebAPI.Exceptions;
using WebAPI.Interfaces;
using WebAPI.Settings;

namespace WebAPI.Services
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly JwtSettings _jwtSettings;

        public JwtTokenService(IOptions<JwtSettings> jwtSettings,
                               UserManager<AppUser> userManager)
        {
            _jwtSettings = jwtSettings.Value;
            _userManager = userManager;
        }

        public async Task<string> GenerateJwtToken(AppUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, role));
            }
            var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signinCredentials = new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                audience: _jwtSettings.Audience,
                issuer: _jwtSettings.Issuer,
                expires: DateTime.Now.AddHours(_jwtSettings.AccessTokenDuration),
                signingCredentials: signinCredentials,
                claims: claims
            );
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public RefreshToken GenerateRefreshToken(string ipAddress)
        {
            var randomNumber = new byte[32];
            RandomNumberGenerator generator = RandomNumberGenerator.Create();
            {
                generator.GetBytes(randomNumber);
                return new RefreshToken
                {
                    Created = DateTime.Now,
                    CreatedByIp = ipAddress,
                    Token = Convert.ToBase64String(randomNumber),
                    Expires = DateTime.Now.AddDays(_jwtSettings.RefreshTokenDuration),
                };
            }

        }

        public AppUser GetUserByRefreshToken(string token)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));
            if (user == null)
                throw new AppException("Invalid token.");

            return user;
        }

        public async Task SaveRefreshToken(RefreshToken token, AppUser user)
        {
            if (user.RefreshTokens == null)
                user.RefreshTokens = new List<RefreshToken>();

            user.RefreshTokens.Add(token);
            await _userManager.UpdateAsync(user);
        }

        public async Task RemoveOldRefreshTokens(AppUser user)
        {
            var oldRefreshTokens = user.RefreshTokens
                .Where(r => !r.IsActive &&
                             r.Created.AddDays(_jwtSettings.RefreshTokenDuration) <= DateTime.Now)
                .ToList();
            foreach (var oldRefreshToken in oldRefreshTokens)
            {
                user.RefreshTokens.Remove(oldRefreshToken);
            }
            await _userManager.UpdateAsync(user);
        }
    }
}
