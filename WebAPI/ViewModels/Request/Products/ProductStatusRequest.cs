using DAL;
using DAL.Entities;
using FluentValidation;
using Microsoft.Extensions.Localization;
using WebAPI.Specifications.Products;

namespace WebAPI.ViewModels.Request.Products
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
        private readonly IStringLocalizer<ValidationResourсes> _validationResources;
        private readonly IRepository<ProductStatus> _productStatusRepository;
        public ProductStatusRequestValidator(IRepository<ProductStatus> productStatusRepository,
            IStringLocalizer<ValidationResourсes> validationResources)
        {
            _validationResources = validationResources;

            _productStatusRepository = productStatusRepository;
            //Name
            RuleFor(x => x.Name).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName(_validationResources["NamePropName"])
               .Length(2, 20)
               .Must(IsUniqueName).WithMessage(_validationResources["ProductStatusUniquesNameMessage"]);
        }

        private bool IsUniqueName(string name)
        {
            var spec = new ProductStatusGetByNameSpecification(name);
            return _productStatusRepository.GetBySpecAsync(spec).Result == null;
        }
    }
}
