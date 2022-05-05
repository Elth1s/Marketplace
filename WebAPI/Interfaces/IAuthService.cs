using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Response;

namespace WebAPI.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> SignInAsync(SignInRequest request, string ipAddress);
        Task<AuthResponse> SignUpAsync(SignUpRequest request, string ipAddress);
        Task<AuthResponse> RefreshTokenAsync(TokenRequest request, string ipAddress);
        Task RevokeTokenAsync(TokenRequest request, string ipAddress);
    }
}
