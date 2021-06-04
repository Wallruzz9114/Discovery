using FluentValidation;

namespace Core.Features.Customers.Commands.Register
{
    public class RegisterCustomerCommandValidator : AbstractValidator<RegisterCustomerCommand>
    {
        public RegisterCustomerCommandValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Customer Name is empty.")
                .Length(2, 100).WithMessage("The Name must have between 2 and 100 characters.");

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("Customer Email is empty.")
                .Length(2, 100).WithMessage("The name must be between 2 and 100 characters.");
        }
    }
}