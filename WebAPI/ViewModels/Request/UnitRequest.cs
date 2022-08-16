using FluentValidation;
using Microsoft.Extensions.Localization;

namespace WebAPI.ViewModels.Request
{
    /// <summary>
    /// Unit class to create and update unit 
    /// </summary>
    public class UnitRequest
    {
        /// <summary>
        /// English unit measure
        /// </summary>
        /// <example>m</example>
        public string EnglishMeasure { get; set; }
        /// <summary>
        /// Ukrainian unit measure
        /// </summary>
        /// <example>м</example>
        public string UkrainianMeasure { get; set; }
    }

    /// <summary>
    /// Class for <seealso cref="UnitRequest" /> validation
    /// </summary>
    public class UnitRequestValidator : AbstractValidator<UnitRequest>
    {
        private readonly IStringLocalizer<ValidationResourсes> _validationResources;
        public UnitRequestValidator(IStringLocalizer<ValidationResourсes> validationResources)
        {
            _validationResources = validationResources;

            //English Measure
            RuleFor(x => x.EnglishMeasure).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName(_validationResources["EnglishMeasurePropName"]).WithMessage(_validationResources["RequiredMessage"])
               .Length(1, 30);
            //Ukrainian Measure
            RuleFor(x => x.UkrainianMeasure).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName(_validationResources["UkrainianMeasurePropName"]).WithMessage(_validationResources["RequiredMessage"])
               .Length(1, 30);
        }
    }
}
