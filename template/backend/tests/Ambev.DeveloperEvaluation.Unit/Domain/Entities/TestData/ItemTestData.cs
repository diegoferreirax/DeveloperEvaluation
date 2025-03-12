using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

public static class ItemTestData
{
    private static readonly Faker<Item> ItemFaker = new Faker<Item>()
        .RuleFor(u => u.Id, f => Guid.NewGuid())
        .RuleFor(u => u.Product, f => f.Commerce.Product())
        .RuleFor(u => u.UnitPrice, f => f.Random.Number(1, 99));

    public static Item GenerateValidItem()
    {
        return ItemFaker.Generate();
    }

    public static Item[] GenerateValidItemsList()
    {
        return new List<Item> ()
        {
            ItemFaker.Generate(),
            ItemFaker.Generate()
        }.ToArray();
    }
}
