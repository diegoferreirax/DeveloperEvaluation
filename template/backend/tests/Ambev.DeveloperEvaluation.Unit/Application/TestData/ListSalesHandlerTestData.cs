using Ambev.DeveloperEvaluation.Application.Sales.v1.ListSales;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

public static class ListSalesHandlerTestData
{
    private static readonly Faker<ListSalesCommand> listSalesCommandFaker = new Faker<ListSalesCommand>()
        .RuleFor(u => u.Page, f => 1)
        .RuleFor(u => u.Size, f => f.Random.Number(10, 20))
        .RuleFor(u => u.Order, f => f.Company.CompanyName());

    public static ListSalesCommand GenerateValidCommand()
    {
        return listSalesCommandFaker.Generate();
    }
}
