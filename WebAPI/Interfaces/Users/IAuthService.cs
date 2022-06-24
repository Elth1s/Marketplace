using WebAPI.ViewModels.Request.Users;
using WebAPI.ViewModels.Response;

namespace WebAPI.Interfaces.Users
{
    public interface IAuthService
    {
        Task<AuthResponse> SignInAsync(SignInRequest request, string ipAddress);
        Task<AuthResponse> SignUpAsync(SignUpRequest request, string ipAddress);
        Task<AuthResponse> RefreshTokenAsync(TokenRequest request, string ipAddress);
        Task RevokeTokenAsync(TokenRequest request, string ipAddress);
        Task<AuthResponse> GoogleExternalLoginAsync(ExternalLoginRequest request, string ipAddress);
        Task<AuthResponse> FacebookExternalLoginAsync(ExternalLoginRequest request, string ipAddress);
    }
}
