using FluentValidation;
using Microsoft.Extensions.Localization;
using WebAPI.Helpers;

namespace WebAPI.ViewModels.Request.Shops
{
    /// <summary>
    /// Shop class to create and update shop
    /// </summary>
    public class UpdateShopRequest
    {
        /// <summary>
        /// Name of shop
        /// </summary>
        /// <example>Smith's Shop</example>
        public string Name { get; set; }
        /// <summary>
        /// Shop URL
        /// </summary>
        /// <example>https://some_shop_example_url.com</example>
        public string SiteUrl { get; set; }
        /// <summary>
        /// Seller full name
        /// </summary>
        /// <example>Nick Smith</example>
        public string FullName { get; set; }
        /// <summary>
        /// Shop email address
        /// </summary>
        /// <example>shop@gmail.com</example>
        public string Email { get; set; }
        /// <summary>
        /// Shop description
        /// </summary>
        /// <example>Some description</example>
        public string Description { get; set; }
        /// <summary>
        /// Shop photo
        /// </summary>
        /// <example>https://some_shop_image_example.jp</example>
        public string Photo { get; set; }
        /// <summary>
        /// Country identifier
        /// </summary>
        /// <example>1</example>
        public int? CountryId { get; set; }
        /// <summary>
        /// City identifier
        /// </summary>
        /// <example>1</example>
        public int? CityId { get; set; }

        /// <summary>
        /// List of phones
        /// </summary>
        public List<ShopPhoneRequest> Phones { get; set; }
    }

    public class ShopPhoneRequest
    {
        /// <summary>
        /// Phone number
        /// </summary>
        /// <example>+380 50 638 8216</example>
        public string Phone { get; set; }
        /// <summary>
        /// Comment for phone number
        /// </summary>
        /// <example>Some comment</example>
        public string Comment { get; set; }
    }

    /// <summary>
    /// Class for <seealso cref="UpdateShopRequest" /> validation
    /// </summary>
    public class UpdateShopRequestValidator : AbstractValidator<UpdateShopRequest>
    {
        private readonly IStringLocalizer<ValidationResourсes> _validationResources;
        private readonly PhoneNumberManager _phoneNumberManager;
        public UpdateShopRequestValidator(
            IStringLocalizer<ValidationResourсes> validationResources, PhoneNumberManager phoneNumberManager)
        {
            _validationResources = validationResources;
            _phoneNumberManager = phoneNumberManager;

            //Name
            RuleFor(x => x.Name).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName(_validationResources["NamePropName"])
               .Length(2, 30);

            //SiteUrl
            RuleFor(x => x.SiteUrl).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName(_validationResources["SiteURLPropName"]).WithMessage(_validationResources["RequiredMessage"]);

            //FullName
            RuleFor(x => x.FullName).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName(_validationResources["FullNamePropName"]).WithMessage(_validationResources["PluralRequiredMessage"])
               .Length(2, 70).WithMessage(_validationResources["PluralLengthMessage"]);

            //Email
            RuleFor(x => x.Email).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName(_validationResources["EmailOrPhonePropName"])
               .EmailAddress();

            //Description
            RuleFor(x => x.Description).Cascade(CascadeMode.Stop)
              .Length(10, 850).When(d => !string.IsNullOrEmpty(d.Description)).WithName(_validationResources["DescriptionNameProp"]);

            //Phones
            RuleForEach(s => s.Phones)
                .ChildRules(child =>
            {
                child.RuleFor(x => x.Phone)
                        .NotEmpty().WithName(_validationResources["PhonePropName"])
                        .Must(IsValidPhone).WithMessage(_validationResources["InvalidFormatMessage"]);

                child.RuleFor(x => x.Comment)
                        .MaximumLength(20).WithName(_validationResources["CommentMaxLengthPropName"]);
            }).When(s => s.Phones.Count > 0);
        }
        private bool IsValidPhone(string phone)
        {
            return _phoneNumberManager.IsValidNumber(phone);
        }
    }
}
