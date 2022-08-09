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
        /// Name of filter name
        /// </summary>
        /// <example>Brand name</example>
        public string Name { get; set; }
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
            //Name
            RuleFor(x => x.Name).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName(_validationResources["NamePropName"])
               .Length(2, 30);
        }
    }
}
