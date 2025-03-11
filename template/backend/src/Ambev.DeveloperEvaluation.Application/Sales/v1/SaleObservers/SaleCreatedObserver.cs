using Ambev.DeveloperEvaluation.Application.Sales.v1.RegisterSale;
using Ambev.DeveloperEvaluation.Domain.Events;

namespace Ambev.DeveloperEvaluation.Application.Sales.v1.SaleObservers;

public class SaleCreatedObserver : IEventObserver
{
    public async Task HandleEvent(object eventData)
    {
        var result = eventData as RegisterSaleResult;
        Console.WriteLine("----------");
        Console.WriteLine($"SaleCreatedObserver event data: {result.id}");
        await Task.FromResult(0);
    }
}
