﻿using AutoMapper;
using DAL.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System.Drawing.Imaging;
using WebAPI.Constants;
using WebAPI.Exceptions;
using WebAPI.Extensions;
using WebAPI.Helpers;
using WebAPI.Interfaces;
using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Response;

namespace WebAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public UserService(IMapper mapper, UserManager<AppUser> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task UpdateProfileAsync(string id, UpdateProfileRequest request)
        {
            var userObject = await _userManager.FindByNameAsync(request.UserName);

            userObject.UserWithUserNameExistChecking(id);

            var user = await _userManager.FindByIdAsync(id);

            user.UserNullChecking();

            if (request.Photo != null)
            {
                var filePath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    request.Photo.Replace(ImagePath.RequestUsersImagePath,
                    ImagePath.UsersImagePath));
                if (!File.Exists(filePath))
                {
                    if (user.Photo != null)
                    {
                        filePath = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           ImagePath.UsersImagePath,
                           user.Photo);

                        if (File.Exists(filePath))
                            File.Delete(filePath);
                    }
                    var img = ImageWorker.FromBase64StringToImage(request.Photo);
                    string randomFilename = Path.GetRandomFileName() + ".jpg";
                    var dir = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.UsersImagePath, randomFilename);
                    img.Save(dir, ImageFormat.Jpeg);

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
            return response;
        }

        public async Task ChangePasswordAsync(string id, ChangePasswordRequest request)
        {
            var user = await _userManager.FindByIdAsync(id);
            user.UserNullChecking();

            var resultPasswordCheck = await _userManager.CheckPasswordAsync(user, request.OldPassword);
            if (!resultPasswordCheck)
                throw new AppException("Invalid password");

            var resultPasswordUpdate = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.Password);
            if (!resultPasswordUpdate.Succeeded)
                throw new AppException("Updating password failed");
        }
    }
}
