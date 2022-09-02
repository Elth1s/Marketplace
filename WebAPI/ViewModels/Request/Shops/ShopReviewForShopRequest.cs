using FluentValidation;
using Microsoft.Extensions.Localization;

namespace WebAPI.ViewModels.Request.Shops
{
    /// <summary>
    /// Shop review for shop class to pagination shop review
    /// </summary>
    public class ShopReviewForShopRequest
    {
        /// <summary>
        /// Shop identifier
        /// </summary>
        /// <example>1</example>
        public int ShopId { get; set; }
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
    /// Class for <seealso cref="ShopReviewForShopRequest" /> validation
    /// </summary>
    public class ShopReviewForShopRequestRequestValidation : AbstractValidator<ShopReviewForShopRequest>
    {
        private readonly IStringLocalizer<ValidationResourсes> _validationResources;
        public ShopReviewForShopRequestRequestValidation(IStringLocalizer<ValidationResourсes> validationResources)
        {
            _validationResources = validationResources;

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
