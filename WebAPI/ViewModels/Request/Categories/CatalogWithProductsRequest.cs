using FluentValidation;
using Microsoft.Extensions.Localization;

namespace WebAPI.ViewModels.Request.Categories
{
    /// <summary>
    /// Category class to get category with product
    /// </summary>
    public class CatalogWithProductsRequest
    {
        /// <summary>
        /// Url of category
        /// </summary>
        /// <example>technology-and-electronics</example>
        public string UrlSlug { get; set; }
        /// <summary>
        /// Page
        /// </summary>
        /// <example>1</example>
        public int? Page { get; set; }
        /// <summary>
        /// Min price
        /// </summary>
        /// <example>100</example>
        public int? Min { get; set; }
        /// <summary>
        /// Max price
        /// </summary>
        /// <example>10000</example>
        public int? Max { get; set; }
        /// <summary>
        /// Row per page
        /// </summary>
        /// <example>40</example>
        public int RowsPerPage { get; set; }
        /// <summary>
        /// List identifier of filters
        /// </summary>
        public IEnumerable<int> Filters { get; set; }
    }

    /// <summary>
    /// Class for <seealso cref="CatalogWithProductsRequest" /> validation
    /// </summary>
    public class CatalogWithProductsRequestValidation : AbstractValidator<CatalogWithProductsRequest>
    {
        private readonly IStringLocalizer<ValidationResourсes> _validationResources;
        public CatalogWithProductsRequestValidation(IStringLocalizer<ValidationResourсes> validationResources)
        {
            _validationResources = validationResources;

            //UrlSlug
            RuleFor(a => a.UrlSlug).Cascade(CascadeMode.Stop)
                 .NotEmpty().WithName(_validationResources["CategoryUrlSlugPropName"])
                 .WithMessage(_validationResources["RequiredMessage"]);

            //Page
            RuleFor(a => a.Page).Cascade(CascadeMode.Stop)
                .NotEmpty().WithName(_validationResources["PagePropName"])
                .WithMessage(_validationResources["RequiredMessage"])
                .GreaterThan(0).WithMessage(_validationResources["GreaterThanMessage"]);

            //RowsPerPage
            RuleFor(a => a.RowsPerPage).Cascade(CascadeMode.Stop)
                .NotEmpty().WithName(_validationResources["RowsPerPagePropName"])
                .WithMessage(_validationResources["RequiredMessage"])
                .GreaterThan(0).WithMessage(_validationResources["GreaterThanMessage"]);
        }
    }
}
