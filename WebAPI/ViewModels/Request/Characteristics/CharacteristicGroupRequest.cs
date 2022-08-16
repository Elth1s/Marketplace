using FluentValidation;
using Microsoft.Extensions.Localization;

namespace WebAPI.ViewModels.Request.Characteristics
{
    /// <summary>
    /// Characteristic group class to create and update characteristic group
    /// </summary>
    public class CharacteristicGroupRequest
    {
        /// <summary>
        /// Name of characteristic
        /// </summary>
        /// <example>Main</example>
        public string Name { get; set; }
    }

    /// <summary>
    /// Class for <seealso cref="CharacteristicGroupRequest" /> validation
    /// </summary>
    public class CharacteristicGroupRequestValidation : AbstractValidator<CharacteristicGroupRequest>
    {
        private readonly IStringLocalizer<ValidationResourсes> _validationResources;

        public CharacteristicGroupRequestValidation(IStringLocalizer<ValidationResourсes> validationResources)
        {
            _validationResources = validationResources;

            //Name
            RuleFor(x => x.Name).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName(_validationResources["NamePropName"])
               .Length(2, 30);
        }
    }
}
