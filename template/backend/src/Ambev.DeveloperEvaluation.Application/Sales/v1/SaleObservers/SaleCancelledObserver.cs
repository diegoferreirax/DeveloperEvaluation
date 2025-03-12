using Ambev.DeveloperEvaluation.Domain.Events;

namespace Ambev.DeveloperEvaluation.Application.Sales.v1.SaleObservers;

public class SaleCancelledObserver : IEventObserver
{
    public async Task HandleEvent(object eventData)
    {
        Console.WriteLine("----------");
        Console.WriteLine($"SaleCancelledObserver event data: {eventData}");
        await Task.FromResult(0);
    }
}
