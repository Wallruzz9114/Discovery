using MediatR;

namespace Core.Base.Queries
{
    public interface IQuery<out TResult> : IRequest<TResult> { }
}