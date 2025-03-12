namespace Ambev.DeveloperEvaluation.Domain.Events;

public class EventPublisher
{
    private readonly List<IEventObserver> _observers = new List<IEventObserver>();

    public EventPublisher() { }

    public EventPublisher(IEventObserver observer)
    {
        _observers.Add(observer);
    }

    public void RegisterObserver(IEventObserver observer)
    {
        _observers.Add(observer);
    }

    public void UnregisterObserver(IEventObserver observer)
    {
        _observers.Remove(observer);
    }

    public async Task Notify(object eventData)
    {
        foreach (var observer in _observers)
        {
            await observer.HandleEvent(eventData).ConfigureAwait(false);
        }
    }
}
