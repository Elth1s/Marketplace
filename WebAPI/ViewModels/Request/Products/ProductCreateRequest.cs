﻿using FluentValidation;
using Microsoft.Extensions.Localization;

namespace WebAPI.ViewModels.Request.Products
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
        /// <summary>
        /// Product characteristics value
        /// </summary>
        public IEnumerable<CharacteristicValueProductCreate> CharacteristicsValue { get; set; }
        /// <summary>
        /// Product filters value
        /// </summary>
        public IEnumerable<ImageProductCreate> Images { get; set; }
    }

    /// <summary>
    /// Filter value class to create a product 
    /// </summary>
    public class FilterValueProductCreate
    {
        /// <summary>
        /// Filter name identifier
        /// </summary>
        /// <example>1</example>
        public int NameId { get; set; }
        /// <summary>
        /// Filter value identifier
        /// </summary>
        /// <example>1</example>
        public int ValueId { get; set; }
        /// <summary>
        /// Custom value in the product filter
        /// </summary>
        /// <example>15.7</example>
        public float? CustomValue { get; set; }
    }

    /// <summary>
    /// Filter value class to create a product 
    /// </summary>
    public class CharacteristicValueProductCreate
    {
        /// <summary>
        /// Characteristic name identifier
        /// </summary>
        /// <example>1</example>
        public int NameId { get; set; }
        /// <summary>
        /// Characteristic value identifier
        /// </summary>
        /// <example>1</example>
        public int ValueId { get; set; }
    }

    /// <summary>
    /// FilterValue class to create a product 
    /// </summary>
    public class ImageProductCreate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Priority { get; set; }
    }

    /// <summary>
    /// Class for <seealso cref="ProductCreateRequest" /> validation
    /// </summary>
    public class ProductCreateRequestValidator : AbstractValidator<ProductCreateRequest>
    {
        private readonly IStringLocalizer<ValidationResourсes> _validationResources;
        public ProductCreateRequestValidator(IStringLocalizer<ValidationResourсes> validationResources)
        {
            _validationResources = validationResources;

            //Name
            RuleFor(x => x.Name).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName(_validationResources["NamePropName"])
               .Length(2, 120);

            //Description
            RuleFor(x => x.Description).Cascade(CascadeMode.Stop)
              .Length(10, 850).When(d => !string.IsNullOrEmpty(d.Description)).WithName(_validationResources["DescriptionNameProp"]);

            //Price
            RuleFor(c => c.Price).Cascade(CascadeMode.Stop)
                .NotEmpty().WithName(_validationResources["PriceNameProp"]).WithMessage(_validationResources["RequiredMessage"])
                .InclusiveBetween(0.1f, 99_999_999_999_999f);

            //Count
            RuleFor(a => a.Count).Cascade(CascadeMode.Stop)
                .NotEmpty().WithName(_validationResources["CountPropName"]).WithMessage(_validationResources["RequiredMessage"])
                .InclusiveBetween(0, 999_999_999);
        }
    }
}
