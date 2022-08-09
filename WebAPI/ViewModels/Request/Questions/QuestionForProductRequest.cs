using FluentValidation;
using Microsoft.Extensions.Localization;

namespace WebAPI.ViewModels.Request.Questions
{
    /// <summary>
    /// Question for product class to pagination question
    /// </summary>
    public class QuestionForProductRequest
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
    /// Class for <seealso cref="QuestionForProductRequest" /> validation
    /// </summary>
    public class QuestionForProductRequestValidation : AbstractValidator<QuestionForProductRequest>
    {
        private readonly IStringLocalizer<ValidationResourсes> _validationResources;
        public QuestionForProductRequestValidation(IStringLocalizer<ValidationResourсes> validationResources)
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
