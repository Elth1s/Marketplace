using WebAPI.ViewModels.Request.Users;
using WebAPI.ViewModels.Response;

namespace WebAPI.Interfaces.Users
{
    public interface IUserService
    {
        Task<ProfileResponse> GetProfileAsync(string id);

        Task UpdateProfileAsync(string id, UpdateProfileRequest request);
    }
}
