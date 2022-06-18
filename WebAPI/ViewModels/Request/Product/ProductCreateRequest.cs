using FluentValidation;

namespace WebAPI.ViewModels.Request
{
    /// <summary>
    /// Product class to create product 
    /// </summary>
    public class ProductCreateRequest
    {
        /// <summary>
        /// Name of product
        /// </summary>
        /// <example>T-shirt</example>
        public string Name { get; set; }
        /// <summary>
        /// Product description
        /// </summary>
        /// <example>Some description for product</example>
        public string Description { get; set; }
        /// <summary>
        /// Product price
        /// </summary>
        /// <example>5000</example>
        public float Price { get; set; }
        /// <summary>
        /// Product count
        /// </summary>
        /// <example>15</example>
        public int Count { get; set; }
        /// <summary>
        /// Product shop identifier
        /// </summary>
        /// <example>1</example>
        public int ShopId { get; set; }
        /// <summary>
        /// Product status identifier
        /// </summary>
        /// <example>2</example>
        public int StatusId { get; set; }
        /// <summary>
        /// Product category identifier
        /// </summary>
        /// <example>3</example>
        public int CategoryId { get; set; }
        /// <summary>
        /// Product filters value
        /// </summary>
        public IEnumerable<FilterValueProductCreate> FiltersValue { get; set; }
    }

    /// <summary>
    /// Filter value class to create a product 
    /// </summary>
    public class FilterValueProductCreate
    {
        /// <summary>
        /// Filter value identifier
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }
        /// <summary>
        /// Custom value in the product filter
        /// </summary>
        /// <example>15.7</example>
        public float? CustomValue { get; set; }
    }

    /// <summary>
    /// Class for <seealso cref="ProductCreateRequest" /> validation
    /// </summary>
    public class ProductCreateRequestValidator : AbstractValidator<ProductCreateRequest>
    {
        public ProductCreateRequestValidator()
        {
            //Name
            RuleFor(x => x.Name).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName("Name").WithMessage("{PropertyName} is required")
               .Length(2, 20).WithMessage("{PropertyName} should be between 2 and 20 characters");

            //Description
            RuleFor(x => x.Description).Cascade(CascadeMode.Stop)
              .NotEmpty().WithName("Description").WithMessage("{PropertyName} is required")
              .Length(15, 250).WithMessage("{PropertyName} should be between 15 and 250 characters");

            //Price
            RuleFor(c => c.Price).Cascade(CascadeMode.Stop)
                .NotEmpty().WithName("Price").WithMessage("{PropertyName} is required!")
               .InclusiveBetween(0.1f, float.MaxValue).WithMessage("{PropertyName} should be greater than 0.1");

            //Count
            RuleFor(a => a.Count).Cascade(CascadeMode.Stop)
                .NotEmpty().WithName("Count").WithMessage("{PropertyName} is required!")
                .InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} should be greater than 1");
        }
    }
}
