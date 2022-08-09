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
        /// Name of country
        /// </summary>
        /// <example>Ukraine</example>
        public string Name { get; set; }
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

            //Name
            RuleFor(x => x.Name).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName(_validationResources["NamePropName"])
               .Length(2, 60);

            //Code
            RuleFor(c => c.Code).Cascade(CascadeMode.Stop)
                   .NotEmpty().WithMessage(_validationResources["CodePropName"])
                   .Length(2).WithMessage(_validationResources["CodeExactLengthMessage"]);
        }

    }
}
