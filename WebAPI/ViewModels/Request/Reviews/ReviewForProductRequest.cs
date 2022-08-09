using FluentValidation;
using Microsoft.Extensions.Localization;

namespace WebAPI.ViewModels.Request.Reviews
{
    /// <summary>
    /// Review for product class to pagination review
    /// </summary>
    public class ReviewForProductRequest
    {
        /// <summary>
        /// Product slug
        /// </summary>
        /// <example>some-product-slug</example>
        public string ProductSlug { get; set; }
        /// <summary>
        /// Page
        /// </summary>
        /// <example>1</example>
        public int Page { get; set; }

        /// <summary>
        /// Row per page
        /// </summary>
        /// <example>8</example>
        public int RowsPerPage { get; set; }
    }

    /// <summary>
    /// Class for <seealso cref="ReviewForProductRequest" /> validation
    /// </summary>
    public class ReviewForProductRequestValidation : AbstractValidator<ReviewForProductRequest>
    {
        private readonly IStringLocalizer<ValidationResourсes> _validationResources;
        public ReviewForProductRequestValidation(IStringLocalizer<ValidationResourсes> validationResources)
        {
            _validationResources = validationResources;

            //UrlSlug
            RuleFor(a => a.ProductSlug).Cascade(CascadeMode.Stop)
                 .NotEmpty().WithName(_validationResources["ProductUrlSlugPropName"])
                 .WithMessage(_validationResources["RequiredMessage"]);

            //Page
            RuleFor(a => a.Page).Cascade(CascadeMode.Stop)
                .NotEmpty().WithName(_validationResources["PagePropName"])
                .WithMessage(_validationResources["RequiredMessage"])
                .GreaterThan(0).WithMessage(_validationResources["GreaterThanMessage"]);

            //RowsPerPage
            RuleFor(a => a.RowsPerPage).Cascade(CascadeMode.Stop)
                .NotEmpty().WithName(_validationResources["RowsPerPagePropName"])
                .WithMessage(_validationResources["PluralRequiredMessage"])
                .GreaterThan(0).WithMessage(_validationResources["PluralGreaterThanMessage"]);
        }
    }
}
