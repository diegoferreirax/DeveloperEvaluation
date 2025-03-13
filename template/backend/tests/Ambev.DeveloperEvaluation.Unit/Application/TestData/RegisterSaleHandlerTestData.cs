using Ambev.DeveloperEvaluation.Application.Sales.v1.RegisterSale;
using Ambev.DeveloperEvaluation.Domain.Validation;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

public static class RegisterSaleHandlerTestData
{
    private static readonly Faker<RegisterSaleItemCommand> registerSaleItemHandlerFaker = new Faker<RegisterSaleItemCommand>()
        .RuleFor(u => u.ItemId, f => Guid.NewGuid())
        .RuleFor(u => u.Quantity, f => f.Random.Number(1, ItemValidator.MaxLimitQuantityByItem))
        .RuleFor(u => u.Discount, f => f.Random.Number(1, 20));

    private static readonly Faker<RegisterSaleCommand> registerSaleHandlerFaker = new Faker<RegisterSaleCommand>()
        .RuleFor(u => u.CustomerId, f => Guid.NewGuid())
        .RuleFor(u => u.SaleNumber, f => f.Random.Number(100, 999))
        .RuleFor(u => u.SaleDate, f => f.Date.Future(0))
        .RuleFor(u => u.IsCanceled, f => false)
        .RuleFor(u => u.Branch, f => f.Company.CompanyName())
        .RuleFor(u => u.SaleItens, f => new List<RegisterSaleItemCommand>() 
        { 
            registerSaleItemHandlerFaker,
            registerSaleItemHandlerFaker
        });

    public static RegisterSaleCommand GenerateValidCommand()
    {
        return registerSaleHandlerFaker.Generate();
    }
}
