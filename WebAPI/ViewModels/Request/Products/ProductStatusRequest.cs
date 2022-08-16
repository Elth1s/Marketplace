using FluentValidation;
using Microsoft.Extensions.Localization;

namespace WebAPI.ViewModels.Request.Products
{
    /// <summary>
    /// Product status class to create and update product status 
    /// </summary>
    public class ProductStatusRequest
    {
        /// <summary>
        /// English name of the product status
        /// </summary>
        /// <example>In stock</example>
        public string EnglishName { get; set; }
        /// <summary>
        /// Ukrainian name of product status
        /// </summary>
        /// <example>В наявності</example>
        public string UkrainianName { get; set; }
    }

    /// <summary>
    /// Class for <seealso cref="ProductStatusRequest" /> validation
    /// </summary>
    public class ProductStatusRequestValidator : AbstractValidator<ProductStatusRequest>
    {
        private readonly IStringLocalizer<ValidationResourсes> _validationResources;
        public ProductStatusRequestValidator(IStringLocalizer<ValidationResourсes> validationResources)
        {
            _validationResources = validationResources;

            //English Name
            RuleFor(x => x.EnglishName).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName(_validationResources["EnglishNamePropName"]).WithMessage(_validationResources["RequiredMessage"])
               .Length(2, 20);
            //Ukrainian Name
            RuleFor(x => x.UkrainianName).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName(_validationResources["UkrainianNamePropName"]).WithMessage(_validationResources["RequiredMessage"])
               .Length(2, 20);
        }
    }
}
