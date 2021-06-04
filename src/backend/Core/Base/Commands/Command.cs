
using FluentValidation.Results;

namespace Core.Base.Commands
{
    public abstract class Command<TResult> : ICommand<TResult>
    {
        protected Command()
        {
            ValidationResult = new ValidationResult();
        }

        public ValidationResult ValidationResult { get; set; }

        public virtual bool IsValid() => ValidationResult.IsValid;
    }
}