using FluentValidation;
using Microsoft.Extensions.Localization;

namespace WebAPI.ViewModels.Request.Orders
{
    /// <summary>
    /// Delivery type class to create delivery type
    /// </summary>
    public class DeliveryTypeRequest
    {
        /// <summary>
        /// English name of the delivery type
        /// </summary>
        /// <example>By courier</example>
        public string EnglishName { get; set; }
        /// <summary>
        /// Ukrainian name of the delivery type
        /// </summary>
        /// <example>Кур'єром</example>
        public string UkrainianName { get; set; }
        /// <summary>
        /// Delivery type dark icon
        /// </summary>
        public string DarkIcon { get; set; }
        /// <summary>
        /// Delivery type light icon
        /// </summary>
        public string LightIcon { get; set; }
    }

    /// <summary>
    /// Class for <seealso cref="DeliveryTypeRequest" /> validation
    /// </summary>
    public class DeliveryTypeRequestValidator : AbstractValidator<DeliveryTypeRequest>
    {
        private readonly IStringLocalizer<ValidationResourсes> _validationResources;
        public DeliveryTypeRequestValidator(IStringLocalizer<ValidationResourсes> validationResources)
        {
            _validationResources = validationResources;

            //English Name
            RuleFor(x => x.EnglishName).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName(_validationResources["EnglishNamePropName"]).WithMessage(_validationResources["RequiredMessage"])
               .Length(2, 25);
            //Ukrainian Name
            RuleFor(x => x.UkrainianName).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName(_validationResources["UkrainianNamePropName"]).WithMessage(_validationResources["RequiredMessage"])
               .Length(2, 25);
        }
    }

}
