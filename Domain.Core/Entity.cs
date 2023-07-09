using FluentValidation;

namespace Domain.Core
{
    public abstract class Entity<T> where T : Entity<T>
    {
        protected abstract AbstractValidator<T> Validator { get; }

        public virtual bool IsValid()
        {
            var validator = Validator;
            var validationResult = validator.Validate((T)this);
            return validationResult.IsValid;
        }
    }
}