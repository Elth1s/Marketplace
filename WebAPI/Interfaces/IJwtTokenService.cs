using DAL.Entities;
using DAL.Entities.Identity;

namespace WebAPI.Interfaces
{
    public interface IJwtTokenService
    {
        Task<string> GenerateJwtToken(AppUser user);

        RefreshToken GenerateRefreshToken(string ipAddress);

        Task RemoveOldRefreshTokens(AppUser user);

        Task SaveRefreshToken(RefreshToken token, AppUser user);

        AppUser GetUserByRefreshToken(string token);

    }
}
