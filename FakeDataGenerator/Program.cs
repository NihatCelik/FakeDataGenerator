using Bogus;

var faker = new Faker();
var customerAddress = new Faker<CustomerAddress>("tr")
                    .RuleFor(c => c.Id, (f, c) => f.IndexFaker + 1)
                    .RuleFor(c => c.Address, (f, c) => f.Address.StreetAddress(true));

var customers = new Faker<Customer>("tr")
                    .RuleFor(c => c.Id, (f, c) => f.IndexFaker + 1)
                    .RuleFor(c => c.Name, (f, c) => f.Name.FullName())
                    .RuleFor(c => c.Email, (f, c) => f.Internet.Email())
                    .RuleFor(c => c.CustomerAddresses, f => customerAddress.Generate(f.Random.Int(min: 1, max: 5)));

var customerData = customers.Generate(3);

foreach (var customer in customerData)
{
    Console.WriteLine($"Id: {customer.Id}, Name: {customer.Name}, Email: {customer.Email}\n");
    foreach (var address in customer.CustomerAddresses)
    {
        Console.WriteLine($"Id: {address.Id}, Full Address: {address.Address}");
    }
    Console.WriteLine("------------------------------");
}

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public List<CustomerAddress> CustomerAddresses { get; set; }
}

public class CustomerAddress
{
    public int Id { get; set; }
    public string Address { get; set; }
}