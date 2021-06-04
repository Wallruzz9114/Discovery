using FluentValidation.Results;
using MediatR;

namespace Core.Base.Commands
{
    public interface ICommand<out TResult> : IRequest<CommandHandlerResult>
    {
        ValidationResult ValidationResult { get; set; }
        bool IsValid();
    }
}