﻿using DAL.Entities;
using DAL.Entities.Identity;
using System.Net;
using WebAPI.Exceptions;
using WebAPI.Resources;

namespace WebAPI.Extensions
{
    public static class ExtensionMethods
    {
        public static void UserNullChecking(this AppUser user)
        {
            if (user == null)
            {
                throw new AppException(
                    ErrorMessages.UserNotFound,
                    HttpStatusCode.NotFound);
            }
        }

        public static void UserWithUserNameExistChecking(this AppUser user, string id)
        {
            if (user != null && user.Id != id)
            {
                throw new AppException(
                    ErrorMessages.UsernameAlreadyExists);
            }
        }

        public static void RefreshTokenNotActiveChecking(this RefreshToken refreshToken)
        {
            if (!refreshToken.IsActive)
            {
                throw new AppException(ErrorMessages.InvalidToken);
            }
        }

        public static void RefreshTokenRevokedChecking(this RefreshToken refreshToken)
        {
            if (refreshToken.IsRevoked)
            {
                throw new AppException(ErrorMessages.TokenRevorked);
            }
        }
    }
}
