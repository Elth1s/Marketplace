using DAL;
using DAL.Entities;
using FluentValidation;
using WebAPI.Specifications.Orders;

namespace WebAPI.ViewModels.Request.Order
{
    public class OrderStatusRequest
    {
        /// <summary>
        /// Name of order status
        /// </summary>
        /// <example>"Active"</example>
        public string Name { get; set; }
    }

    /// <summary>
    /// Class for <seealso cref="OrderStatusRequest" /> validation
    /// </summary>
    public class OrderStatusRequestValidator : AbstractValidator<OrderStatusRequest>
    {
        private readonly IRepository<OrderStatus> _orderStatusRepository;
        public OrderStatusRequestValidator(IRepository<OrderStatus> orderStatusRepository)
        {
            _orderStatusRepository = orderStatusRepository;
            //Name
            RuleFor(x => x.Name).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName("Name").WithMessage("{PropertyName} is required")
               .Must(IsUniqueName).WithMessage("Order status with this {PropertyName} already exists")
               .Length(2, 20).WithMessage("{PropertyName} should be between 2 and 20 characters");
        }

        private bool IsUniqueName(string name)
        {
            var spec = new OrderStatusGetByNameSpecification(name);
            return _orderStatusRepository.GetBySpecAsync(spec).Result == null;
        }
    }

}
