namespace Ambev.DeveloperEvaluation.Domain.Events;

public interface IEventObserver
{
    Task HandleEvent(object eventData);
}
