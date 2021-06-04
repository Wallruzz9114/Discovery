using System;
using System.Collections.Generic;
using Core.Base.Commands;
using Core.ViewModels;

namespace Core.Features.Customers.Commands.PlaceOrder
{
    public class PlaceOrderCommand : Command<CommandHandlerResult>
    {
        public Guid CustomerId { get; private set; }
        public List<ProductViewModel> Products { get; private set; }
        public string Currency { get; private set; }

        public PlaceOrderCommand(Guid customerId, List<ProductViewModel> products, string currency)
        {
            CustomerId = customerId;
            Products = products;
            Currency = currency;
        }

        public override bool IsValid()
        {
            ValidationResult = new PlaceOrderCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}