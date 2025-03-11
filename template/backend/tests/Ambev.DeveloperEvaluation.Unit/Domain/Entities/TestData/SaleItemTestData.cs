using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

public static class SaleItemTestData
{
    private static readonly Faker<SaleItem> SaleItemFaker = new Faker<SaleItem>()
        .RuleFor(u => u.Id, f => Guid.NewGuid())
        .RuleFor(u => u.ItemId, f => Guid.NewGuid())
        .RuleFor(u => u.Quantity, f => f.Random.Number(1, 99))
        .RuleFor(u => u.Discount, f => f.Random.Number(1, 10));

    public static SaleItem GenerateValidSaleItem()
    {
        return SaleItemFaker.Generate();
    }

    public static SaleItem[] GenerateValidSaleItemsList()
    {
        return new List<SaleItem> ()
        {
            SaleItemFaker.Generate(),
            SaleItemFaker.Generate()
        }.ToArray();
    }
}
