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
        /// English name of the city
        /// </summary>
        /// <example>Atlanta</example>
        public string EnglishName { get; set; }
        /// <summary>
        /// Ukrainian name of the city
        /// </summary>
        /// <example>Атланта</example>
        public string UkrainianName { get; set; }
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

            //English Name
            RuleFor(x => x.EnglishName).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName(_validationResources["EnglishNamePropName"]).WithMessage(_validationResources["RequiredMessage"])
               .Length(2, 30);
            //Ukrainian Name
            RuleFor(x => x.UkrainianName).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName(_validationResources["UkrainianNamePropName"]).WithMessage(_validationResources["RequiredMessage"])
               .Length(2, 30);
        }
    }
}
