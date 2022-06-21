using FluentValidation;

namespace WebAPI.ViewModels.Request.Characteristics
{
    /// <summary>
    /// Characteristic class to create and update characteristic name
    /// </summary>
    public class CharacteristicNameRequest
    {
        /// <summary>
        /// Name of characteristic name
        /// </summary>
        /// <example>Size</example>
        public string Name { get; set; }
        /// <summary>
        /// Characteristic Group identifier
        /// </summary>
        /// <example>1</example>
        public int CharacteristicGroupId { get; set; }
        /// <summary>
        /// Unit identifier
        /// </summary>
        /// <example>1</example>
        public int? UnitId { get; set; }
    }

    /// <summary>
    /// Class for <seealso cref="CharacteristicNameRequest" /> validation
    /// </summary>
    public class CharacteristicNameRequestValidator : AbstractValidator<CharacteristicNameRequest>
    {
        public CharacteristicNameRequestValidator()
        {
            //Name
            RuleFor(x => x.Name).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName("Name").WithMessage("{PropertyName} is required")
               .Length(2, 30).WithMessage("{PropertyName} should be between 2 and 30 characters");
        }
    }
}
