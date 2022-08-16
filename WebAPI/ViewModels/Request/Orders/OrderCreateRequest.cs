using FluentValidation;
using Microsoft.Extensions.Localization;
using WebAPI.Helpers;

namespace WebAPI.ViewModels.Request.Orders
{
    /// <summary>
    /// Order class to create order
    /// </summary>
    public class OrderCreateRequest
    {
        /// <summary>
        /// Consumer First Name
        /// </summary>
        /// <example>Nick</example>
        public string ConsumerFirstName { get; set; }
        /// <summary>
        /// Consumer Second Name
        /// </summary>
        /// <example>Smith</example>
        public string ConsumerSecondName { get; set; }
        /// <summary>
        /// Consumer Phone
        /// </summary>
        /// <example>+380 50 638 8216</example>
        public string ConsumerPhone { get; set; }
        /// <summary>
        /// Consumer Phone
        /// </summary>
        /// <example>email@gmail.com</example>
        public string ConsumerEmail { get; set; }

        public IEnumerable<OrderProductCreate> OrderProductsCreate { get; set; }
    }

    public class OrderProductCreate
    {
        /// <summary>
        ///  Count Order Product
        /// </summary>
        /// <example>1</example>
        public int Count { get; set; }
        /// <summary>
        /// Product Id
        /// </summary>
        /// <example>1</example>
        public int ProductId { get; set; }
    }


    /// <summary>
    /// Class for <seealso cref="OrderCreateRequest" /> validation
    /// </summary>
    public class OrderCreateRequestValidator : AbstractValidator<OrderCreateRequest>
    {
        private readonly IStringLocalizer<ValidationResourсes> _validationResources;
        private readonly PhoneNumberManager _phoneNumberManager;

        public OrderCreateRequestValidator(IStringLocalizer<ValidationResourсes> validationResources,
            PhoneNumberManager phoneNumberManager)
        {
            _validationResources = validationResources;
            _phoneNumberManager = phoneNumberManager;

            //Consumer First Name
            RuleFor(x => x.ConsumerFirstName).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName(_validationResources["FirstNamePropName"])
               .Length(2, 15);

            //Consumer Second Name
            RuleFor(x => x.ConsumerSecondName).Cascade(CascadeMode.Stop)
              .NotEmpty().WithName(_validationResources["SecondNamePropName"])
              .Length(2, 40);

            //Consumer Phone
            RuleFor(x => x.ConsumerPhone).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName(_validationResources["PhonePropName"])
               .Must(IsValidPhone).WithMessage(_validationResources["InvalidFormatMessage"]);

            //Consumer Email
            RuleFor(x => x.ConsumerEmail).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName(_validationResources["EmailAddressPropName"]).WithMessage(_validationResources["RequiredMessage"])
               .EmailAddress();

        }
        private bool IsValidPhone(string phone)
        {
            return _phoneNumberManager.IsValidNumber(phone);
        }
    }
}




