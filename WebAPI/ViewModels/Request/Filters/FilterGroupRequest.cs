using FluentValidation;
using Microsoft.Extensions.Localization;

namespace WebAPI.ViewModels.Request.Filters
{
    /// <summary>
    /// Filter group class to create and update filter group
    /// </summary>
    public class FilterGroupRequest
    {
        /// <summary>
        /// English name of the filter group
        /// </summary>
        /// <example>Main</example>
        public string EnglishName { get; set; }
        /// <summary>
        /// Ukrainian name of the filter group
        /// </summary>
        /// <example>Основна</example>
        public string UkrainianName { get; set; }
    }

    /// <summary>
    /// Class for <seealso cref="FilterGroupRequest" /> validation
    /// </summary>
    public class FilterGroupRequestValidator : AbstractValidator<FilterGroupRequest>
    {
        private readonly IStringLocalizer<ValidationResourсes> _validationResources;
        public FilterGroupRequestValidator(IStringLocalizer<ValidationResourсes> validationResources)
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
