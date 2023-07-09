using Domain.Core;
using FluentValidation;

namespace Domain.Customers;

public class Customer : Entity<Customer>
{
    public Guid Id { get; protected set; }
    public string? Name { get; protected set; }
    public int Age { get; protected set; }

    protected override AbstractValidator<Customer> Validator
        => new CustomerValidation();

    protected Customer() { }

    protected Customer(string name, int age)
    {
        Id = Guid.NewGuid();
        Name = name;
        Age = age;
    }

    public static class Builder
    {
        public static Customer Create(string name, int age) 
            => new(name, age);
    }
}
