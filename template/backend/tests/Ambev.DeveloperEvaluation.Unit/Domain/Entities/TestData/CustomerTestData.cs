using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

public static class CustomerTestData
{
    private static readonly Faker<Customer> CustomerFaker = new Faker<Customer>()
        .RuleFor(u => u.Name, f => f.Name.FirstName());

    public static Customer GenerateValidCustomer()
    {
        return CustomerFaker.Generate();
    }
}
