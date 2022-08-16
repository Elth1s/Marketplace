using FluentValidation;
using Microsoft.Extensions.Localization;

namespace WebAPI.ViewModels.Request.Filters
{
    /// <summary>
    /// Filter value class to create and update filter value
    /// </summary>
    public class FilterValueRequest
    {
        /// <summary>
        /// English value of the filter value
        /// </summary>
        /// <example>Discrete</example>
        public string EnglishValue { get; set; }
        /// <summary>
        /// Ukrainian value of the filter value
        /// </summary>
        /// <example>Дискретна</example>
        public string UkrainianValue { get; set; }
        /// <summary>
        /// Minimum for custom value
        /// </summary>
        /// <example>null</example>
        public int? Min { get; set; }
        /// <summary>
        /// Maximum for custom value
        /// </summary>
        /// <example>null</example>
        public int? Max { get; set; }
        /// <summary>
        /// Filter name identifier
        /// </summary>
        /// <example>1</example>
        public int FilterNameId { get; set; }
    }

    /// <summary>
    /// Class for <seealso cref="FilterValueRequest" /> validation
    /// </summary>
    public class FilterValueRequestValidator : AbstractValidator<FilterValueRequest>
    {
        private readonly IStringLocalizer<ValidationResourсes> _validationResources;
        public FilterValueRequestValidator(IStringLocalizer<ValidationResourсes> validationResources)
        {
            _validationResources = validationResources;

            //English Value
            RuleFor(x => x.EnglishValue).Cascade(CascadeMode.Stop);

            RuleFor(x => x.UkrainianValue).Cascade(CascadeMode.Stop);

            //Min
            RuleFor(a => a.Min).Cascade(CascadeMode.Stop);
            //.NotEmpty().When(a => a.Value == null || a.Max != null).WithName("Min").WithMessage("{PropertyName} is required!")
            //.Empty().When(a => a.Value != null).WithMessage("{PropertyName} must be empty!");

            //Max
            RuleFor(a => a.Max).Cascade(CascadeMode.Stop);
            //.NotEmpty().When(a => a.Value == null || a.Min != null).WithName("Max").WithMessage("{PropertyName} is required!")
            //.Empty().When(a => a.Value != null).WithMessage("{PropertyName} must be empty!")
            //.GreaterThan(a => a.Min).When(a => a.Min != null).WithMessage("{PropertyName} must after min!");
        }
    }
}
