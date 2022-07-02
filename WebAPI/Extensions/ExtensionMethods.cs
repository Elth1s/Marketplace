using DAL.Entities;
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

        public static void UserEmailConfirmedChecking(this AppUser user)
        {
            if (user.EmailConfirmed)
            {
                throw new AppException(
                    ErrorMessages.AlreadyComfirmEmail, HttpStatusCode.Unauthorized);
            }
        }

        public static void UserPhoneConfirmedChecking(this AppUser user)
        {
            if (user.PhoneNumberConfirmed)
            {
                throw new AppException(
                    ErrorMessages.AlreadyComfirmPhone, HttpStatusCode.Unauthorized);
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


        public static void CategoryNullChecking(this Category category)
        {
            if (category == null)
            {
                throw new AppException(
                    ErrorMessages.CategoryNotFound,
                    HttpStatusCode.NotFound);
            }
        }


        public static void BasketItemNullChecking(this BasketItem basketItem)
        {
            if (basketItem == null)
            {
                throw new AppException(
                    ErrorMessages.BasketItemNotFound,
                    HttpStatusCode.NotFound);
            }
        }

        public static void ShopNullChecking(this Shop shop)
        {
            if (shop == null)
            {
                throw new AppException(
                    ErrorMessages.ShopNotFound,
                    HttpStatusCode.NotFound);
            }
        }


        public static void CharacteristicGroupNullChecking(this CharacteristicGroup characteristicGroup)
        {
            if (characteristicGroup == null)
            {
                throw new AppException(
                    ErrorMessages.CharacteristicGroupNotFound,
                    HttpStatusCode.NotFound);
            }
        }

        public static void CountryNullChecking(this Country country)
        {
            if (country == null)
            {
                throw new AppException(
                    ErrorMessages.CountryNotFound,
                    HttpStatusCode.NotFound);
            }
        }


        public static void CharacteristicNameNullChecking(this CharacteristicName characteristicName)
        {
            if (characteristicName == null)
            {
                throw new AppException(
                    ErrorMessages.CharacteristicNameNotFound,
                    HttpStatusCode.NotFound);
            }
        }

        public static void CharacteristicValueNullChecking(this CharacteristicValue characteristicValue)
        {
            if (characteristicValue == null)
            {
                throw new AppException(
                    ErrorMessages.CharacteristicValueNotFound,
                    HttpStatusCode.NotFound);
            }
        }

        public static void CityNullChecking(this City city)
        {
            if (city == null)
            {
                throw new AppException(
                    ErrorMessages.CityNotFound,
                    HttpStatusCode.NotFound);
            }
        }

        public static void FilterGroupNullChecking(this FilterGroup filterGroup)
        {
            if (filterGroup == null)
            {
                throw new AppException(
                    ErrorMessages.FilterGroupNotFound,
                    HttpStatusCode.NotFound);
            }
        }

        public static void FilterNameNullChecking(this FilterName filterName)
        {
            if (filterName == null)
            {
                throw new AppException(
                    ErrorMessages.FilterNameNotFound,
                    HttpStatusCode.NotFound);
            }
        }

        public static void FilterValueNullChecking(this FilterValue filterValue)
        {
            if (filterValue == null)
            {
                throw new AppException(
                    ErrorMessages.FilterValueNotFound,
                    HttpStatusCode.NotFound);
            }
        }

        public static void ProductStatusNullChecking(this ProductStatus productStatus)
        {
            if (productStatus == null)
            {
                throw new AppException(
                    ErrorMessages.ProductStatusNotFound,
                    HttpStatusCode.NotFound);
            }
        }

        public static void ProductNullChecking(this Product product)
        {
            if (product == null)
            {
                throw new AppException(
                    ErrorMessages.ProductNotFound,
                    HttpStatusCode.NotFound);
            }
        }

        public static void ProductImageNullChecking(this ProductImage productImage)
        {
            if (productImage == null)
            {
                throw new AppException(
                    ErrorMessages.ProductImageNotFound,
                    HttpStatusCode.NotFound);
            }
        }

        public static void UnitNullChecking(this Unit unit)
        {
            if (unit == null)
            {
                throw new AppException(
                    ErrorMessages.UnitNotFound,
                    HttpStatusCode.NotFound);
            }
        }
    }
}
