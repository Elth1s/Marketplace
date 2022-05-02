using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Response;

namespace WebAPI.Intefaces
{
    public interface IAuthService
    {
        Task<AuthResponse> SignInAsync(SignInRequest request, string ipAddress);
        Task<AuthResponse> SignUpAsync(SignUpRequest request, string ipAddress);
        Task<AuthResponse> RefreshAccessTokenAsync(TokenRequest request, string ipAddress);
        Task RevokeToken(TokenRequest request, string ipAddress);
    }
}
