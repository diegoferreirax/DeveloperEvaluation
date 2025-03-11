using Ambev.DeveloperEvaluation.Domain.Events;

namespace Ambev.DeveloperEvaluation.Application.Sales.v1.SaleObservers;

public class SaleModifiedObserver : IEventObserver
{
    public async Task HandleEvent(object eventData)
    {
        Console.WriteLine("----------");
        Console.WriteLine($"SaleModifiedObserver event data: {eventData}");
        await Task.FromResult(0);
    }
}
