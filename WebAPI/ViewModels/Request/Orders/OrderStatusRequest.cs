using FluentValidation;
using Microsoft.Extensions.Localization;

namespace WebAPI.ViewModels.Request.Orders
{
    public class OrderStatusRequest
    {
        /// <summary>
        /// English name of the order status
        /// </summary>
        /// <example>In Process</example>
        public string EnglishName { get; set; }
        /// <summary>
        /// Ukrainian name of order status
        /// </summary>
        /// <example>В процесі</example>
        public string UkrainianName { get; set; }
    }

    /// <summary>
    /// Class for <seealso cref="OrderStatusRequest" /> validation
    /// </summary>
    public class OrderStatusRequestValidator : AbstractValidator<OrderStatusRequest>
    {
        private readonly IStringLocalizer<ValidationResourсes> _validationResources;
        public OrderStatusRequestValidator(IStringLocalizer<ValidationResourсes> validationResources)
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
