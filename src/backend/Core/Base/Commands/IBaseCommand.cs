using FluentValidation.Results;
using MediatR;

namespace Core.Base.Commands
{
    public interface IBaseCommand : IRequest
    {
        ValidationResult ValidationResult { get; set; }
        bool IsValid();
    }
}