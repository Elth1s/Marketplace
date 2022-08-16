using FluentValidation;
using Microsoft.Extensions.Localization;

namespace WebAPI.ViewModels.Request.Filters
{
    /// <summary>
    /// Filter name class to create and update filter name
    /// </summary>
    public class FilterNameRequest
    {
        /// <summary>
        /// English name of the filter name
        /// </summary>
        /// <example>Brand name</example>
        public string EnglishName { get; set; }
        /// <summary>
        /// Ukrainian name of the filter name
        /// </summary>
        /// <example>Назва бренду</example>
        public string UkrainianName { get; set; }
        /// <summary>
        /// Filter group identifier
        /// </summary>
        /// <example>1</example>
        public int FilterGroupId { get; set; }
        /// <summary>
        /// Unit identifier
        /// </summary>
        /// <example>1</example>
        public int? UnitId { get; set; }
    }

    /// <summary>
    /// Class for <seealso cref="FilterNameRequest" /> validation
    /// </summary>
    public class FilterNameRequestValidator : AbstractValidator<FilterNameRequest>
    {
        private readonly IStringLocalizer<ValidationResourсes> _validationResources;
        public FilterNameRequestValidator(IStringLocalizer<ValidationResourсes> validationResources)
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
