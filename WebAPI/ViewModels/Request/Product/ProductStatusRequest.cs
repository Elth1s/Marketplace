using FluentValidation;

namespace WebAPI.ViewModels.Request
{
    /// <summary>
    /// Product status class to create and update product status 
    /// </summary>
    public class ProductStatusRequest
    {
        /// <summary>
        /// Name of product status
        /// </summary>
        /// <example>In stock</example>
        public string Name { get; set; }
    }

    /// <summary>
    /// Class for <seealso cref="ProductStatusRequest" /> validation
    /// </summary>
    public class ProductStatusRequestValidator : AbstractValidator<ProductStatusRequest>
    {
        public ProductStatusRequestValidator()
        {
            //Name
            RuleFor(x => x.Name).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName("Name").WithMessage("{PropertyName} is required")
               .Length(2, 20).WithMessage("{PropertyName} should be between 2 and 20 characters");
        }
    }
}
