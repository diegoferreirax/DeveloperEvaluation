using Ambev.DeveloperEvaluation.Application.Sales.v1.UpdateSale;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

public static class UpdateSaleHandlerTestData
{
    private static readonly Faker<UpdateSaleItemCommand> updateSaleItemHandlerFaker = new Faker<UpdateSaleItemCommand>()
        .RuleFor(u => u.ItemId, f => Guid.NewGuid())
        .RuleFor(u => u.Quantity, f => f.Random.Number(1, 20))
        .RuleFor(u => u.Discount, f => f.Random.Number(1, 10));

    private static readonly Faker<UpdateSaleCommand> updateSaleHandlerFaker = new Faker<UpdateSaleCommand>()
        .RuleFor(u => u.Id, f => Guid.NewGuid())
        .RuleFor(u => u.SaleDate, f => f.Date.Future(0))
        .RuleFor(u => u.IsCanceled, f => false)
        .RuleFor(u => u.Branch, f => f.Company.CompanyName());

    public static UpdateSaleCommand GenerateValidCommand()
    {
        return updateSaleHandlerFaker.Generate();
    }

    public static UpdateSaleItemCommand GenerateValidItemCommand()
    {
        return updateSaleItemHandlerFaker.Generate();
    }

    public static UpdateSaleItemCommand[] GenerateValidItemsCommand()
    {
        return new List<UpdateSaleItemCommand>()
        {
            updateSaleItemHandlerFaker.Generate(),
            updateSaleItemHandlerFaker.Generate()
        }.ToArray();
    }
}
