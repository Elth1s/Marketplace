using DAL;
using DAL.Entities;
using FluentValidation;
using Microsoft.Extensions.Localization;
using WebAPI.Specifications.Shops;

namespace WebAPI.ViewModels.Request.Shops
{
    /// <summary>
    /// Shop class to create and update shop
    /// </summary>
    public class ShopRequest
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
        /// <example>Admin Adminovich</example>
        public string FullName { get; set; }
        /// <summary>
        /// Shop email address
        /// </summary>
        /// <example>shop@gmail.com</example>
        public string Email { get; set; }
        /// <summary>
        /// User password
        /// </summary>
        /// <example>QWEqwe123_</example>
        public string Password { get; set; }
    }

    /// <summary>
    /// Class for <seealso cref="ShopRequest" /> validation
    /// </summary>
    public class ShopRequestValidator : AbstractValidator<ShopRequest>
    {
        private readonly IStringLocalizer<ValidationResourсes> _validationResources;
        private readonly IRepository<Shop> _shopRepository;
        public ShopRequestValidator(IRepository<Shop> shopRepository,
            IStringLocalizer<ValidationResourсes> validationResources)
        {
            _shopRepository = shopRepository;
            _validationResources = validationResources;

            //Name
            RuleFor(x => x.Name).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName(_validationResources["NamePropName"])
               .Length(2, 30);

            //SiteUrl
            RuleFor(x => x.SiteUrl).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName(_validationResources["SiteURLPropName"]).WithMessage(_validationResources["RequiredMessage"])
               .Must(IsUniqueSiteUrl).WithMessage(_validationResources["ShopUniqueSiteURLMessage"]);

            //FullName
            RuleFor(x => x.FullName).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName(_validationResources["FullNamePropName"]).WithMessage(_validationResources["PluralRequiredMessage"])
               .Length(2, 70).WithMessage(_validationResources["PluralLengthMessage"]);

            //Email
            RuleFor(x => x.Email).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName(_validationResources["EmailOrPhonePropName"])
               .EmailAddress()
               .Must(IsUniqueEmail).WithMessage(_validationResources["ShopUniqueEmailMessage"]);

            //Password
            RuleFor(x => x.Password)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithName(_validationResources["PasswordPropName"]);

        }
        private bool IsUniqueEmail(string email)
        {
            var spec = new ShopGetByEmailSpecification(email);
            return _shopRepository.GetBySpecAsync(spec).Result == null;
        }
        private bool IsUniqueSiteUrl(string siteUrl)
        {
            var spec = new ShopGetBySiteUrlSpecification(siteUrl);
            return _shopRepository.GetBySpecAsync(spec).Result == null;
        }
    }
}
