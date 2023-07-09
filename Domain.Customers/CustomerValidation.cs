using FluentValidation;

namespace Domain.Customers
{
    public class CustomerValidation : AbstractValidator<Customer>
    {
        public CustomerValidation()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name must have a maximum of 100 characters");

            RuleFor(c => c.Age)
                .InclusiveBetween(18, 100).WithMessage("Age must be between 18 and 100");
        }
    }
}