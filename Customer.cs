public class Customer
{
    public Guid Id { get; protected set; }
    public string Name { get; protected set; }
    public int Age { get; protected set; }

    protected Customer() { }

    protected Customer(string name, int age)
    {
        Id = Guid.NewGuid();
        Name = name;
        Age = age;
    }
}
