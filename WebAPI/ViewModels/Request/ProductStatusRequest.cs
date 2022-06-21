using DAL;
using DAL.Entities;
using FluentValidation;
using WebAPI.Specifications;

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
        private readonly IRepository<ProductStatus> _productStatusRepository;
        public ProductStatusRequestValidator(IRepository<ProductStatus> productStatusRepository)
        {
            _productStatusRepository = productStatusRepository;
            //Name
            RuleFor(x => x.Name).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName("Name").WithMessage("{PropertyName} is required")
               .Must(IsUniqueName).WithMessage("Product status with this {PropertyName} already exists")
               .Length(2, 20).WithMessage("{PropertyName} should be between 2 and 20 characters");
        }

        private bool IsUniqueName(string name)
        {
            var spec = new ProductStatusGetByNameSpecification(name);
            return _productStatusRepository.GetBySpecAsync(spec).Result == null;
        }
    }
}
