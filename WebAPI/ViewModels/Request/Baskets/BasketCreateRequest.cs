using FluentValidation;
using Microsoft.Extensions.Localization;

namespace WebAPI.ViewModels.Request.Baskets
{
    /// <summary>
    /// Basket class to create basket 
    /// </summary>
    public class BasketCreateRequest
    {
        /// <summary>
        /// Product url slug
        /// </summary>
        /// <example>some-url-slug</example>
        public string UrlSlug { get; set; }

    }

    /// <summary>
    /// Class for <seealso cref="BasketCreateRequest" /> validation
    /// </summary>
    public class BasketCreateRequestValidation : AbstractValidator<BasketCreateRequest>
    {
        private readonly IStringLocalizer<ValidationResourсes> _validationResources;
        public BasketCreateRequestValidation(IStringLocalizer<ValidationResourсes> validationResources)
        {
            _validationResources = validationResources;


            //ProductUrlSlug
            RuleFor(a => a.UrlSlug).Cascade(CascadeMode.Stop)
                 .NotEmpty().WithName(_validationResources["ProductUrlSlugPropName"]);
        }
    }
}
