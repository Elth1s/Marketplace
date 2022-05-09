using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Response;

namespace WebAPI.Interfaces
{
    public interface IUserService
    {
        Task<ProfileResponse> GetProfileAsync(string id);

        Task UpdateProfileAsync(string id, UpdateProfileRequest request);

        Task ChangePasswordAsync(string id, ChangePasswordRequest request);
    }
}
