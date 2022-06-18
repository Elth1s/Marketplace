using DAL;
using DAL.Entities;
using FluentValidation;
using WebAPI.Specifications;

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
        /// Shop description
        /// </summary>
        /// <example>Some shop description</example>
        public string Description { get; set; }
        /// <summary>
        /// Shop image
        /// </summary>
        public string Photo { get; set; }
        /// <summary>
        /// Shop email address
        /// </summary>
        /// <example>shop@gmail.com</example>
        public string Email { get; set; }
        /// <summary>
        /// Shop URL
        /// </summary>
        /// <example>https://some_shop_example_url.com</example>
        public string SiteUrl { get; set; }
        /// <summary>
        /// City identifier
        /// </summary>
        /// <example>1</example>
        public int CityId { get; set; }
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

            //Description
            RuleFor(x => x.Description).Cascade(CascadeMode.Stop)
              .NotEmpty().WithName("Description").WithMessage("{PropertyName} is required")
              .Length(15, 140).WithMessage("{PropertyName} should be between 15 and 140 characters");

            //Email
            RuleFor(x => x.Email).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName("Email address").WithMessage("{PropertyName} is required")
               .EmailAddress().WithMessage("Invalid format of {PropertyName}")
               .Must(IsUniqueEmail).WithMessage("Shop with this {PropertyName} already exists");

            //SiteUrl
            RuleFor(x => x.Email).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName("Site URL").WithMessage("{PropertyName} is required")
               .Must(IsUniqueSiteUrl).WithMessage("Shop with this {PropertyName} already exists");


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
