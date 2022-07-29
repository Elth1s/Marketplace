using DAL;
using DAL.Entities;
using FluentValidation;
using WebAPI.Specifications.Shops;

namespace WebAPI.ViewModels.Request
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
        private readonly IRepository<Shop> _shopRepository;
        public ShopRequestValidator(IRepository<Shop> shopRepository)
        {
            _shopRepository = shopRepository;
            //Name
            RuleFor(x => x.Name).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName("Name").WithMessage("{PropertyName} is required")
               .Length(2, 30).WithMessage("{PropertyName} should be between 2 and 30 characters");

            //SiteUrl
            RuleFor(x => x.Email).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName("Site URL").WithMessage("{PropertyName} is required")
               .Must(IsUniqueSiteUrl).WithMessage("Shop with this {PropertyName} already exists");

            //FullName
            RuleFor(x => x.FullName).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName("FullName").WithMessage("{PropertyName} is required")
               .Length(2, 70).WithMessage("{PropertyName} should be between 2 and 70 characters");

            //Email
            RuleFor(x => x.Email).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName("Email address").WithMessage("{PropertyName} is required")
               .EmailAddress().WithMessage("Invalid format of {PropertyName}")
               .Must(IsUniqueEmail).WithMessage("Shop with this {PropertyName} already exists");

            //Password
            RuleFor(x => x.Password)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithName("Password").WithMessage("{PropertyName} is required");

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
