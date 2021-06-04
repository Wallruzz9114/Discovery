using FluentValidation;
using Models.Base;

namespace Core.Features.Customers.Commands.PlaceOrder
{
    public class PlaceOrderCommandValidator : AbstractValidator<PlaceOrderCommand>
    {
        public PlaceOrderCommandValidator()
        {
            RuleFor(x => x.CustomerId).NotEmpty().WithMessage("CustomerId is empty.");
            RuleFor(x => x.Products).NotEmpty().WithMessage("Products list is empty.");
            RuleFor(x => x.Currency)
                .Must(c => Currency.SupportedCurrencies().Contains(c))
                .WithMessage("At least one product has invalid currency.");
        }
    }
}