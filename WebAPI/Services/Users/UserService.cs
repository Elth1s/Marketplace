using AutoMapper;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System.Drawing.Imaging;
using WebAPI.Constants;
using WebAPI.Exceptions;
using WebAPI.Extensions;
using WebAPI.Helpers;
using WebAPI.Interfaces.Users;
using WebAPI.Resources;
using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Request.Users;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Users;

namespace WebAPI.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly PhoneNumberManager _phoneNumberManager;
        public UserService(IMapper mapper, UserManager<AppUser> userManager, PhoneNumberManager phoneNumberManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _phoneNumberManager = phoneNumberManager;
        }

        public async Task UpdateProfileAsync(string id, UpdateProfileRequest request)
        {
            var user = await _userManager.FindByIdAsync(id);
            user.UserNullChecking();

            if (!string.IsNullOrEmpty(request.Photo))
            {
                var filePath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    request.Photo.Replace(ImagePath.RequestUsersImagePath,
                    ImagePath.UsersImagePath));
                if (!File.Exists(filePath))
                {
                    if (!string.IsNullOrEmpty(user.Photo))
                    {
                        filePath = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           ImagePath.UsersImagePath,
                           user.Photo);

                        if (File.Exists(filePath))
                            File.Delete(filePath);
                    }
                    var img = request.Photo.FromBase64StringToImage();
                    string randomFilename = Guid.NewGuid() + ".png";
                    var dir = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.UsersImagePath, randomFilename);
                    img.Save(dir, ImageFormat.Png);

                    user.Photo = randomFilename;
                }
            }

            _mapper.Map(request, user);

            await _userManager.UpdateAsync(user);

            await _userManager.UpdateNormalizedUserNameAsync(user);
        }

        public async Task<ProfileResponse> GetProfileAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            user.UserNullChecking();

            var response = _mapper.Map<ProfileResponse>(user);
            response.HasPassword = await _userManager.HasPasswordAsync(user);
            return response;
        }

        public async Task ChangeEmailAsync(string userId, ChangeEmailRequest request)
        {
            var user = await _userManager.FindByIdAsync(userId);
            user.UserNullChecking();

            var passwordCheck = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!passwordCheck)
                throw new AppException(ErrorMessages.InvalidPassword);

            user.Email = request.Email;

            await _userManager.UpdateAsync(user);
        }

        public async Task ChangePhoneAsync(string userId, ChangePhoneRequest request)
        {
            var user = await _userManager.FindByIdAsync(userId);
            user.UserNullChecking();

            var passwordCheck = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!passwordCheck)
                throw new AppException(ErrorMessages.InvalidPassword);

            user.PhoneNumber = _phoneNumberManager.GetPhoneE164Format(request.Phone);

            await _userManager.UpdateAsync(user);
        }

        public async Task<bool> HasPassword(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            user.UserNullChecking();

            return await _userManager.HasPasswordAsync(user);
        }

        public async Task AddPasswordAsync(string userId, AddPasswordRequest request)
        {
            var user = await _userManager.FindByIdAsync(userId);
            user.UserNullChecking();

            var hasPassword = await _userManager.HasPasswordAsync(user);

            if (hasPassword)
                throw new AppException(ErrorMessages.PasswordExist);

            var resultPasswordAdd = await _userManager.AddPasswordAsync(user, request.Password);
            if (!resultPasswordAdd.Succeeded)
                throw new AppException(ErrorMessages.AddPasswordFailed);
        }

        public async Task ChangePasswordAsync(string userId, ChangePasswordRequest request)
        {
            var user = await _userManager.FindByIdAsync(userId);
            user.UserNullChecking();

            var resultPasswordCheck = await _userManager.CheckPasswordAsync(user, request.OldPassword);
            if (!resultPasswordCheck)
                throw new AppException(ErrorMessages.InvalidPassword);

            var resultPasswordUpdate = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.Password);
            if (!resultPasswordUpdate.Succeeded)
                throw new AppException(ErrorMessages.PasswordUpdateFail);
        }

        public async Task<AdminSearchResponse<UserResponse>> SearchUsersAsync(AdminSearchRequest request)
        {
            var count = _userManager.UserSearch(request.Name, request.IsAscOrder, request.OrderBy).Count();
            var users = _userManager.UserSearch(
                request.Name,
                request.IsAscOrder,
                request.OrderBy,
                (request.Page - 1) * request.RowsPerPage,
                request.RowsPerPage).ToList();

            var mappedUsers = _mapper.Map<IEnumerable<UserResponse>>(users);
            var response = new AdminSearchResponse<UserResponse>() { Count = count, Values = mappedUsers };

            return response;
        }

        public async Task DeleteUsersAsync(IEnumerable<string> ids)
        {
            foreach (var item in ids)
            {
                var user = await _userManager.FindByIdAsync(item);
                await _userManager.DeleteAsync(user);
            }
        }
    }
}
