using FluentValidation;
using Microsoft.Extensions.Localization;

namespace WebAPI.ViewModels.Request.Characteristics
{
    /// <summary>
    /// Characteristic class to create and update characteristic value
    /// </summary>
    public class CharacteristicValueRequest
    {
        /// <summary>
        /// Value of characteristic value
        /// </summary>
        /// <example>1000</example>
        public string Value { get; set; }
        /// <summary>
        /// Characteristic Name identifier
        /// </summary>
        /// <example>1</example>
        public int CharacteristicNameId { get; set; }
    }

    /// <summary>
    /// Class for <seealso cref="CharacteristicValueRequest" /> validation
    /// </summary>
    public class CharacteristicValueRequestValidator : AbstractValidator<CharacteristicValueRequest>
    {
        private readonly IStringLocalizer<ValidationResourсes> _validationResources;
        public CharacteristicValueRequestValidator(IStringLocalizer<ValidationResourсes> validationResources)
        {
            _validationResources = validationResources;

            //Value
            RuleFor(x => x.Value).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName(_validationResources["ValuePropName"])
               .Length(1, 30);
        }
    }
}
