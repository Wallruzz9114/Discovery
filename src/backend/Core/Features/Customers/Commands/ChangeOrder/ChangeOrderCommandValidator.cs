using Core.Features.Customers.Commands.ChangeOrder;
using FluentValidation;
using Models.Base;

namespace Core.Features.Customers.Commands.ChangeOrder
{
    public class ChangeOrderCommandValidator : AbstractValidator<ChangeOrderCommand>
    {
        public ChangeOrderCommandValidator()
        {
            RuleFor(x => x.CustomerId).NotEmpty().WithMessage("CustomerId is empty.");
            RuleFor(x => x.OrderId).NotEmpty().WithMessage("OrderId is empty");
            RuleFor(x => x.Products).NotEmpty().WithMessage("Products list is empty");
            RuleFor(x => x.Currency)
                .Must(c => Currency.SupportedCurrencies().Contains(c))
                .WithMessage("At least one product has invalid currency.");
        }
    }
}