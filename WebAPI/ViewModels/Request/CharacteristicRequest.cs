using FluentValidation;

namespace WebAPI.ViewModels.Request
{
    /// <summary>
    /// Characteristic class to create and update characteristic
    /// </summary>
    public class CharacteristicRequest
    {
        /// <summary>
        /// Name of characteristic
        /// </summary>
        /// <example>Size</example>
        public string Name { get; set; }
        /// <summary>
        /// Characteristic Group identifier
        /// </summary>
        /// <example>1</example>
        public int CharacteristicGroupId { get; set; }
    }

    /// <summary>
    /// Class for <seealso cref="CharacteristicRequest" /> validation
    /// </summary>
    public class CharacteristicRequestValidator : AbstractValidator<CharacteristicRequest>
    {
        public CharacteristicRequestValidator()
        {
            //Name
            RuleFor(x => x.Name).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName("Name").WithMessage("{PropertyName} is required")
               .Length(2, 30).WithMessage("{PropertyName} should be between 2 and 30 characters");
        }
    }
}
