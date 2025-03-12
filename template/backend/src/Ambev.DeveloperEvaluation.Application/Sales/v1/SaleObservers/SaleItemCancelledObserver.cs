using Ambev.DeveloperEvaluation.Domain.Events;

namespace Ambev.DeveloperEvaluation.Application.Sales.v1.SaleObservers;

public class SaleItemCancelledObserver : IEventObserver
{
    public async Task HandleEvent(object eventData)
    {
        Console.WriteLine("----------");
        Console.WriteLine($"SaleItemCancelledObserver event data: {eventData}");
        await Task.FromResult(0);
    }
}
