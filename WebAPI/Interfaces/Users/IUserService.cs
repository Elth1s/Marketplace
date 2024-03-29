﻿using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Request.Users;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Users;

namespace WebAPI.Interfaces.Users
{
    public interface IUserService
    {
        Task<SearchResponse<UserResponse>> SearchUsersAsync(AdminSearchRequest request);
        Task DeleteUsersAsync(IEnumerable<string> ids);
        Task RemoveProfileAsync(string userId);
        Task<ProfileResponse> GetProfileAsync(string id);
        Task<AuthResponse> UpdateProfileAsync(string id, UpdateProfileRequest request, string ipAddress);
        Task<IEnumerable<UserReviewResponse>> GetUserReviewsAsync(string userId);

        Task ChangeEmailAsync(string userId, ChangeEmailRequest request);
        Task ChangePhoneAsync(string userId, ChangePhoneRequest request);

        Task GoogleConnectAsync(ExternalLoginRequest request, string userId);
        Task FacebookConnectAsync(ExternalLoginRequest request, string userId);

        Task<bool> HasPassword(string userId);

        Task AddPasswordAsync(string userId, AddPasswordRequest request);
        Task ChangePasswordAsync(string userId, ChangePasswordRequest request);

    }
}
