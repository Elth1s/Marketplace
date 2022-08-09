using FluentValidation;
using Microsoft.Extensions.Localization;

namespace WebAPI.ViewModels.Request.Baskets
{
    /// <summary>
    /// Basket class to update 
    /// </summary>
    public class BasketUpdateRequest
    {
        /// <summary>
        /// Product identifier
        /// </summary>
        /// <example>1</example>
        public int ProductId { get; set; }

        /// <summary>
        /// Count
        /// </summary>
        /// <example>1</example>
        public int Count { get; set; }

    }


    /// <summary>
    /// Class for <seealso cref="BasketUpdateRequest" /> validation
    /// </summary>
    public class BasketUpdateRequestValidation : AbstractValidator<BasketUpdateRequest>
    {
        private readonly IStringLocalizer<ValidationResourсes> _validationResources;
        public BasketUpdateRequestValidation(IStringLocalizer<ValidationResourсes> validationResources)
        {
            _validationResources = validationResources;

            //Count
            RuleFor(a => a.Count).Cascade(CascadeMode.Stop)
                 .NotEmpty().WithName(_validationResources["CountPropName"])
                 .WithMessage(_validationResources["RequiredMessage"])
                 .InclusiveBetween(1, 999_999_999);
        }
    }
}
