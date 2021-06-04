using System;
using System.Collections.Generic;
using Core.Base.Commands;
using Core.ViewModels;

namespace Core.Features.Customers.Commands.ChangeOrder
{
    public class ChangeOrderCommand : Command<CommandHandlerResult>
    {
        public Guid CustomerId { get; protected set; }
        public Guid OrderId { get; protected set; }
        public string Currency { get; protected set; }
        public List<ProductViewModel> Products { get; protected set; }

        public ChangeOrderCommand(Guid customerId, Guid orderId, List<ProductViewModel> products, string currency)
        {
            CustomerId = customerId;
            OrderId = orderId;
            Products = products;
            Currency = currency;
        }

        public override bool IsValid()
        {
            ValidationResult = new ChangeOrderCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}