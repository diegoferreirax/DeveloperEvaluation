using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

public static class SaleTestData
{
    private static readonly Faker<Sale> SaleFaker = new Faker<Sale>()
        .RuleFor(u => u.CustomerId, f => Guid.NewGuid())
        .RuleFor(u => u.Customer, f => CustomerTestData.GenerateValidCustomer())
        .RuleFor(u => u.SaleNumber, f => f.Random.Number(100, 999))
        .RuleFor(u => u.SaleDate, f => f.Date.Future(0))
        .RuleFor(u => u.TotalAmount, f => f.Random.Number(100, 9999))
        .RuleFor(u => u.IsCanceled, f => false)
        .RuleFor(u => u.Branch, f => f.Company.CompanyName());

    public static Sale GenerateValidSale()
    {
        return SaleFaker.Generate();
    }
}
