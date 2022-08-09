using DAL.Entities;
using Microsoft.Extensions.Localization;
using System.Net;
using WebAPI.Exceptions;

namespace WebAPI.Extensions
{
    public static class ExtensionMethods
    {
        static public IStringLocalizerFactory StringLocalizerFactory { set; get; }
        public static void UserNullChecking(this AppUser user)
        {
            if (user == null)
            {
                var factory = StringLocalizerFactory.Create(typeof(ErrorMessages));
                throw new AppException(
                    factory["UserNotFound"],
                    HttpStatusCode.NotFound);
            }
        }

        public static void UserWithUserNameExistChecking(this AppUser user, string id)
        {
            if (user != null && user.Id != id)
            {
                var factory = StringLocalizerFactory.Create(typeof(ErrorMessages));
                throw new AppException(
                    factory["UsernameAlreadyExists"]);
            }
        }

        public static void UserEmailConfirmedChecking(this AppUser user)
        {
            if (user.EmailConfirmed)
            {
                var factory = StringLocalizerFactory.Create(typeof(ErrorMessages));
                throw new AppException(
                    factory["AlreadyComfirmEmail"], HttpStatusCode.Unauthorized);
            }
        }

        public static void UserPhoneConfirmedChecking(this AppUser user)
        {
            if (user.PhoneNumberConfirmed)
            {
                var factory = StringLocalizerFactory.Create(typeof(ErrorMessages));
                throw new AppException(
                    factory["AlreadyComfirmPhone"], HttpStatusCode.Unauthorized);
            }
        }

        public static void RefreshTokenNotActiveChecking(this RefreshToken refreshToken)
        {
            if (!refreshToken.IsActive)
            {
                var factory = StringLocalizerFactory.Create(typeof(ErrorMessages));
                throw new AppException(factory["InvalidToken"]);
            }
        }

        public static void RefreshTokenRevokedChecking(this RefreshToken refreshToken)
        {
            if (refreshToken.IsRevoked)
            {
                var factory = StringLocalizerFactory.Create(typeof(ErrorMessages));
                throw new AppException(factory["TokenRevorked"]);
            }
        }


        public static void CategoryNullChecking(this Category category)
        {
            if (category == null)
            {
                var factory = StringLocalizerFactory.Create(typeof(ErrorMessages));
                throw new AppException(
                    factory["CategoryNotFound"],
                    HttpStatusCode.NotFound);
            }
        }


        public static void BasketItemNullChecking(this BasketItem basketItem)
        {
            if (basketItem == null)
            {
                var factory = StringLocalizerFactory.Create(typeof(ErrorMessages));
                throw new AppException(
                    factory["BasketItemNotFound"],
                    HttpStatusCode.NotFound);
            }
        }

        public static void ShopNullChecking(this Shop shop)
        {
            if (shop == null)
            {
                var factory = StringLocalizerFactory.Create(typeof(ErrorMessages));
                throw new AppException(
                    factory["ShopNotFound"],
                    HttpStatusCode.NotFound);
            }
        }


        public static void CharacteristicGroupNullChecking(this CharacteristicGroup characteristicGroup)
        {
            if (characteristicGroup == null)
            {
                var factory = StringLocalizerFactory.Create(typeof(ErrorMessages));
                throw new AppException(
                    factory["CharacteristicGroupNotFound"],
                    HttpStatusCode.NotFound);
            }
        }

        public static void CountryNullChecking(this Country country)
        {
            if (country == null)
            {
                var factory = StringLocalizerFactory.Create(typeof(ErrorMessages));
                throw new AppException(
                    factory["CountryNotFound"],
                    HttpStatusCode.NotFound);
            }
        }

        public static void CountryCodeChecking(this Country country)
        {
            if (country != null)
            {
                var factory = StringLocalizerFactory.Create(typeof(ErrorMessages));
                throw new AppException(
                   factory["CountryWithCodeExist"]);
            }
        }

        public static void CountryNameChecking(this Country country)
        {
            if (country != null)
            {
                var factory = StringLocalizerFactory.Create(typeof(ErrorMessages));
                throw new AppException(
                    factory["CountryWithNameExist"]);
            }
        }


        public static void CharacteristicNameNullChecking(this CharacteristicName characteristicName)
        {
            if (characteristicName == null)
            {
                var factory = StringLocalizerFactory.Create(typeof(ErrorMessages));
                throw new AppException(
                    factory["CharacteristicNameNotFound"],
                    HttpStatusCode.NotFound);
            }
        }

        public static void CharacteristicValueNullChecking(this CharacteristicValue characteristicValue)
        {
            if (characteristicValue == null)
            {
                var factory = StringLocalizerFactory.Create(typeof(ErrorMessages));
                throw new AppException(
                    factory["CharacteristicValueNotFound"],
                    HttpStatusCode.NotFound);
            }
        }

        public static void CityNullChecking(this City city)
        {
            if (city == null)
            {
                var factory = StringLocalizerFactory.Create(typeof(ErrorMessages));
                throw new AppException(
                    factory["CityNotFound"],
                    HttpStatusCode.NotFound);
            }
        }

        public static void FilterGroupNullChecking(this FilterGroup filterGroup)
        {
            if (filterGroup == null)
            {
                var factory = StringLocalizerFactory.Create(typeof(ErrorMessages));
                throw new AppException(
                    factory["FilterGroupNotFound"],
                    HttpStatusCode.NotFound);
            }
        }

        public static void FilterNameNullChecking(this FilterName filterName)
        {
            if (filterName == null)
            {
                var factory = StringLocalizerFactory.Create(typeof(ErrorMessages));
                throw new AppException(
                    factory["FilterNameNotFound"],
                    HttpStatusCode.NotFound);
            }
        }

        public static void FilterValueNullChecking(this FilterValue filterValue)
        {
            if (filterValue == null)
            {
                var factory = StringLocalizerFactory.Create(typeof(ErrorMessages));
                throw new AppException(
                    factory["FilterValueNotFound"],
                    HttpStatusCode.NotFound);
            }
        }

        public static void ProductStatusNullChecking(this ProductStatus productStatus)
        {
            if (productStatus == null)
            {
                var factory = StringLocalizerFactory.Create(typeof(ErrorMessages));
                throw new AppException(
                    factory["ProductStatusNotFound"],
                    HttpStatusCode.NotFound);
            }
        }

        public static void ProductNullChecking(this Product product)
        {
            if (product == null)
            {
                var factory = StringLocalizerFactory.Create(typeof(ErrorMessages));
                throw new AppException(
                    factory["ProductNotFound"],
                    HttpStatusCode.NotFound);
            }
        }

        public static void OrderNullChecking(this Order order)
        {
            if (order == null)
            {
                var factory = StringLocalizerFactory.Create(typeof(ErrorMessages));
                throw new AppException(
                    factory["OrderNotFound"],
                    HttpStatusCode.NotFound);
            }
        }

        public static void ProductImageNullChecking(this ProductImage productImage)
        {
            if (productImage == null)
            {
                var factory = StringLocalizerFactory.Create(typeof(ErrorMessages));
                throw new AppException(
                    factory["ProductImageNotFound"],
                    HttpStatusCode.NotFound);
            }
        }

        public static void UnitNullChecking(this Unit unit)
        {
            if (unit == null)
            {
                var factory = StringLocalizerFactory.Create(typeof(ErrorMessages));
                throw new AppException(
                    factory["UnitNotFound"],
                    HttpStatusCode.NotFound);
            }
        }

        public static void CategoryNameChecking(this Category category)
        {
            if (category != null)
            {
                var factory = StringLocalizerFactory.Create(typeof(ErrorMessages));
                throw new AppException(
                    factory["CategoryNameNotUnique"]);
            }
        }

        public static void CategoryUrlSlugChecking(this Category category)
        {
            if (category != null)
            {
                var factory = StringLocalizerFactory.Create(typeof(ErrorMessages));
                throw new AppException(
                    factory["CategoryUrlSlugNotUnique"]);
            }
        }

        public static void OrderStatusNullChecking(this OrderStatus orderStatus)
        {
            if (orderStatus == null)
            {
                var factory = StringLocalizerFactory.Create(typeof(ErrorMessages));
                throw new AppException(
                    factory["OrderStatusNotFound"],
                    HttpStatusCode.NotFound);
            }
        }

        public static void QuestionNullChecking(this Question question)
        {
            if (question == null)
            {
                var factory = StringLocalizerFactory.Create(typeof(ErrorMessages));
                throw new AppException(
                    factory["QuestionNotFound"],
                    HttpStatusCode.NotFound);
            }
        }
        public static void QuestionImageNullChecking(this QuestionImage questionImage)
        {
            if (questionImage == null)
            {
                var factory = StringLocalizerFactory.Create(typeof(ErrorMessages));
                throw new AppException(
                    factory["QuestionImageNotFound"],
                    HttpStatusCode.NotFound);
            }
        }

        public static void ReviewImageNullChecking(this ReviewImage reviewImage)
        {
            if (reviewImage == null)
            {
                var factory = StringLocalizerFactory.Create(typeof(ErrorMessages));
                throw new AppException(
                    factory["ReviewImageNotFound"],
                    HttpStatusCode.NotFound);
            }
        }

        public static void ReviewNullChecking(this Review review)
        {
            if (review == null)
            {
                var factory = StringLocalizerFactory.Create(typeof(ErrorMessages));
                throw new AppException(
                    factory["ReviewNotFound"],
                    HttpStatusCode.NotFound);
            }
        }
    }
}
