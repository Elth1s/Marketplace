using WebAPI.ViewModels.Request.Users;
using WebAPI.ViewModels.Response;

namespace WebAPI.Interfaces.Users
{
    public interface IUserService
    {
        Task<ProfileResponse> GetProfileAsync(string id);

        Task UpdateProfileAsync(string id, UpdateProfileRequest request);

        Task ChangeEmailAsync(string userId, ChangeEmailRequest request);

        Task ChangePhoneAsync(string userId, ChangePhoneRequest request);

        Task<bool> HasPassword(string userId);

        Task AddPasswordAsync(string userId, AddPasswordRequest request);

        Task ChangePasswordAsync(string userId, ChangePasswordRequest request);
    }
}
