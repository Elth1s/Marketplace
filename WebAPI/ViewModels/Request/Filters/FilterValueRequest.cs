using FluentValidation;

namespace WebAPI.ViewModels.Request.Filters
{
    /// <summary>
    /// Filter value class to create and update filter value
    /// </summary>
    public class FilterValueRequest
    {
        /// <summary>
        /// Value of filter value
        /// </summary>
        /// <example>AMD</example>
        public string Value { get; set; }
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
        public FilterValueRequestValidator()
        {
            //Value
            RuleFor(x => x.Value).Cascade(CascadeMode.Stop)
                //.NotEmpty().When(a => a.Min == null && a.Max == null).WithName("Value").WithMessage("{PropertyName} is required!")
                //.Empty().When(a => a.Max != null && a.Min != null).WithMessage("{PropertyName} must be empty!")
                .Length(2, 30).WithMessage("{PropertyName} should be between 2 and 30 characters");

            //Min
            RuleFor(a => a.Min).Cascade(CascadeMode.Stop);
            //.NotEmpty().When(a => a.Value == null || a.Max != null).WithName("Min").WithMessage("{PropertyName} is required!")
            //.Empty().When(a => a.Value != null).WithMessage("{PropertyName} must be empty!");

            //Max
            RuleFor(a => a.Max).Cascade(CascadeMode.Stop)
                //.NotEmpty().When(a => a.Value == null || a.Min != null).WithName("Max").WithMessage("{PropertyName} is required!")
                //.Empty().When(a => a.Value != null).WithMessage("{PropertyName} must be empty!")
                .GreaterThan(a => a.Min).When(a => a.Min != null).WithMessage("{PropertyName} must after min!");
        }
    }
}
