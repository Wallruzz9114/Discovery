using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Base.Commands;
using FluentValidation.Results;

namespace Core.Base.Commands
{
    public abstract class CommandHandler<TCommand, TResult> : ICommandHandler<TCommand, TResult> where TCommand : ICommand<CommandHandlerResult>
    {
        protected ValidationResult ValidationResult;

        protected CommandHandler()
        {
            ValidationResult = new ValidationResult();
        }

        public abstract Task<Guid> RunCommand(TCommand command, CancellationToken cancellationToken);

        public async Task<CommandHandlerResult> Handle(TCommand command, CancellationToken cancellationToken)
        {
            var result = new CommandHandlerResult();

            try
            {
                if (command == null) throw new ArgumentNullException(nameof(command));
                if (command.IsValid()) result.Id = await RunCommand(command, cancellationToken);
            }
            catch (Exception exception)
            {
                command.ValidationResult.Errors.Add(new ValidationFailure("Business rule error", exception.Message));
            }

            result.ValidationResult = command.ValidationResult;
            return result;
        }
    }
}