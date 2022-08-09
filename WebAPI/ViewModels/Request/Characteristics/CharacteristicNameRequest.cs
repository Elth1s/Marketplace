using FluentValidation;
using Microsoft.Extensions.Localization;

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
        private readonly IStringLocalizer<ValidationResourсes> _validationResources;
        public CharacteristicNameRequestValidator(IStringLocalizer<ValidationResourсes> validationResources)
        {
            _validationResources = validationResources;

            //Name
            RuleFor(x => x.Name).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName(_validationResources["NamePropName"])
               .Length(2, 30);
        }
    }
}
