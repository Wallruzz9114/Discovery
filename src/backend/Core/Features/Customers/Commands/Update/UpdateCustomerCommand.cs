using System;
using Core.Base.Commands;

namespace Core.Features.Customers.Commands.Update
{
    public class UpdateCustomerCommand : Command<CommandHandlerResult>
    {
        public Guid CustomerId { get; protected set; }
        public string Name { get; protected set; }

        public UpdateCustomerCommand(Guid customerId, string name)
        {
            CustomerId = customerId;
            Name = name;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateCustomerCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}