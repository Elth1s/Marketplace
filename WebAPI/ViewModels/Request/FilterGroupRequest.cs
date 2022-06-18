using FluentValidation;

namespace WebAPI.ViewModels.Request
{
    /// <summary>
    /// Filter group class to create and update filter group
    /// </summary>
    public class FilterGroupRequest
    {
        /// <summary>
        /// Name of filter group
        /// </summary>
        /// <example>Main</example>
        public string Name { get; set; }
    }

    /// <summary>
    /// Class for <seealso cref="FilterGroupRequest" /> validation
    /// </summary>
    public class FilterGroupRequestValidator : AbstractValidator<FilterGroupRequest>
    {
        public FilterGroupRequestValidator()
        {
            //Name
            RuleFor(x => x.Name).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName("Name").WithMessage("{PropertyName} is required")
               .Length(2, 30).WithMessage("{PropertyName} should be between 2 and 30 characters");
        }
    }
}
