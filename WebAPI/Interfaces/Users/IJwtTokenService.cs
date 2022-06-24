using DAL.Entities;
using DAL.Entities.Identity;
using Google.Apis.Auth;
using WebAPI.ViewModels.Request.Users;
using WebAPI.ViewModels.Response;

namespace WebAPI.Interfaces.Users
{
    public interface IJwtTokenService
    {
        Task<string> GenerateJwtToken(AppUser user);

        RefreshToken GenerateRefreshToken(string ipAddress);

        Task RemoveOldRefreshTokens(AppUser user);

        Task SaveRefreshToken(RefreshToken token, AppUser user);

        AppUser GetUserByRefreshToken(string token);

        Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(ExternalLoginRequest request);

        Task<FacebookResponse> VerifyFacebookToken(ExternalLoginRequest request);
    }
}
