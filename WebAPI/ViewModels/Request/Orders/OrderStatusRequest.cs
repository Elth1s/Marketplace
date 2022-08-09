using DAL;
using DAL.Entities;
using FluentValidation;
using Microsoft.Extensions.Localization;
using WebAPI.Specifications.Orders;

namespace WebAPI.ViewModels.Request.Orders
{
    public class OrderStatusRequest
    {
        /// <summary>
        /// Name of order status
        /// </summary>
        /// <example>Active</example>
        public string Name { get; set; }
    }

    /// <summary>
    /// Class for <seealso cref="OrderStatusRequest" /> validation
    /// </summary>
    public class OrderStatusRequestValidator : AbstractValidator<OrderStatusRequest>
    {
        private readonly IStringLocalizer<ValidationResourсes> _validationResources;
        private readonly IRepository<OrderStatus> _orderStatusRepository;
        public OrderStatusRequestValidator(IRepository<OrderStatus> orderStatusRepository,
            IStringLocalizer<ValidationResourсes> validationResources)
        {
            _validationResources = validationResources;
            _orderStatusRepository = orderStatusRepository;
            //Name
            RuleFor(x => x.Name).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName(_validationResources["NamePropName"])
               .Length(2, 20)
               .Must(IsUniqueName).WithMessage(_validationResources["OrderStatusUniqueNameMessage"]);
        }

        private bool IsUniqueName(string name)
        {
            var spec = new OrderStatusGetByNameSpecification(name);
            return _orderStatusRepository.GetBySpecAsync(spec).Result == null;
        }
    }

}
