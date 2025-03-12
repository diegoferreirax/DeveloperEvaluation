using Ambev.DeveloperEvaluation.Application.Sales.v1.ListSaleItems;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

public static class ListSaleItemsHandlerTestData
{
    private static readonly Faker<ListSaleItemsCommand> listSaleItemsCommandFaker = new Faker<ListSaleItemsCommand>()
        .RuleFor(u => u.Id, f => Guid.NewGuid());

    public static ListSaleItemsCommand GenerateValidCommand()
    {
        return listSaleItemsCommandFaker.Generate();
    }
}
