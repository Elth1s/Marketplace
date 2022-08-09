using FluentValidation;
using Microsoft.Extensions.Localization;

namespace WebAPI.ViewModels.Request
{
    /// <summary>
    /// City class to create and update city
    /// </summary>
    public class CityRequest
    {
        /// <summary>
        /// City name
        /// </summary>
        /// <example>Atlanta</example>
        public string Name { get; set; }
        /// <summary>
        /// Country identifier
        /// </summary>
        /// <example>1</example>
        public int CountryId { get; set; }
    }

    /// <summary>
    /// Class for <seealso cref="CityRequest" /> validation
    /// </summary>
    public class CityRequestValidator : AbstractValidator<CityRequest>
    {
        private readonly IStringLocalizer<ValidationResourсes> _validationResources;
        public CityRequestValidator(IStringLocalizer<ValidationResourсes> validationResources)
        {
            _validationResources = validationResources;

            //Name
            RuleFor(x => x.Name).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName(_validationResources["NamePropName"])
               .Length(2, 30);
        }
    }
}
