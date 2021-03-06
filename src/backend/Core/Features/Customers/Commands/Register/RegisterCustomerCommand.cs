using Core.Base.Commands;

namespace Core.Features.Customers.Commands.Register
{
    public class RegisterCustomerCommand : Command<CommandHandlerResult>
    {
        public string Email { get; protected set; }
        public string Password { get; protected set; }
        public string PasswordConfirm { get; protected set; }
        public string Name { get; protected set; }

        public RegisterCustomerCommand(string email, string name, string password, string passwordConfirm)
        {
            Name = name;
            Email = email;
            Password = password;
            PasswordConfirm = passwordConfirm;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterCustomerCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}