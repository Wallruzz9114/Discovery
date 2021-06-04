using Core.Base.Commands;
using MediatR;

namespace Core.Base.Commands
{
    public interface ICommandHandler<in TCommand, TResult> : IRequestHandler<TCommand, CommandHandlerResult> where TCommand : ICommand<CommandHandlerResult> { }
}