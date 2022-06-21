using FluentValidation;

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
        public CharacteristicValueRequestValidator()
        {
            //Value
            RuleFor(x => x.Value).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName("Value").WithMessage("{PropertyName} is required")
               .Length(1, 30).WithMessage("{PropertyName} should be between 1 and 30 characters");
        }
    }
}
