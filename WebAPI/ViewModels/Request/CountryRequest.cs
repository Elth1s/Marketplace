using FluentValidation;
using Microsoft.Extensions.Localization;

namespace WebAPI.ViewModels.Request
{
    /// <summary>
    /// Country class to create and update country 
    /// </summary>
    public class CountryRequest
    {
        /// <summary>
        /// English name of the country
        /// </summary>
        /// <example>Ukraine</example>
        public string EnglishName { get; set; }
        /// <summary>
        /// Ukrainian name of the country
        /// </summary>
        /// <example>Україна</example>
        public string UkrainianName { get; set; }
        /// <summary>
        /// Code of country
        /// </summary>
        /// <example>UA</example>
        public string Code { get; set; }
    }

    /// <summary>
    /// Class for <seealso cref="CountryRequest" /> validation
    /// </summary>
    public class CountryRequestValidator : AbstractValidator<CountryRequest>
    {
        private readonly IStringLocalizer<ValidationResourсes> _validationResources;
        public CountryRequestValidator(IStringLocalizer<ValidationResourсes> validationResources)
        {
            _validationResources = validationResources;
            //English Name
            RuleFor(x => x.EnglishName).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName(_validationResources["EnglishNamePropName"]).WithMessage(_validationResources["RequiredMessage"])
               .Length(2, 60);
            //Ukrainian Name
            RuleFor(x => x.UkrainianName).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName(_validationResources["UkrainianNamePropName"]).WithMessage(_validationResources["RequiredMessage"])
               .Length(2, 60);
            //Code
            RuleFor(c => c.Code).Cascade(CascadeMode.Stop)
                   .NotEmpty().WithMessage(_validationResources["CodePropName"])
                   .Length(2).WithMessage(_validationResources["CodeExactLengthMessage"]);
        }
    }
}
