using System;
using FluentValidation.Results;

namespace Core.Base.Commands
{
    public class CommandHandlerResult
    {
        public Guid Id { get; set; }
        public ValidationResult ValidationResult { get; set; }
    }
}