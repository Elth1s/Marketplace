using FluentValidation;

namespace WebAPI.ViewModels.Request
{
    /// <summary>
    /// Country class to create and update country 
    /// </summary>
    public class AdminSearchRequest
    {
        /// <summary>
        /// Page
        /// </summary>
        /// <example>1</example>
        public int Page { get; set; }

        /// <summary>
        /// Page
        /// </summary>
        /// <example>8</example>
        public int RowsPerPage { get; set; }

        /// <summary>
        /// Name of country
        /// </summary>
        /// <example>USA</example>
        public string Name { get; set; }

        /// <summary>
        /// Is ascending order
        /// </summary>
        /// <example>true</example>
        public bool IsAscOrder { get; set; }

        /// <summary>
        /// Order by
        /// </summary>
        /// <example>Id</example>
        public string OrderBy { get; set; }
    }

    /// <summary>
    /// Class for <seealso cref="AdminSearchRequest" /> validation
    /// </summary>
    public class AdminSearchRequestValidator : AbstractValidator<AdminSearchRequest>
    {
        public AdminSearchRequestValidator()
        {
            //Page
            RuleFor(x => x.Page).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName("Page").WithMessage("{PropertyName} is required")
               .GreaterThan(0).WithMessage("{PropertyName} should be greater than 0");

            //RowsPerPage
            RuleFor(x => x.RowsPerPage).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName("Rows per page").WithMessage("{PropertyName} is required")
               .GreaterThan(0).WithMessage("{PropertyName} should be greater than 0");

            //OrderBy
            RuleFor(x => x.OrderBy).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName("Order by").WithMessage("{PropertyName} is required");
        }
    }
}
