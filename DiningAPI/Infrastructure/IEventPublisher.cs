using DiningAPI.Events;

namespace DiningAPI.Infrastructure;

public interface IEventPublisher
{
    Task PublishAsync<T>(T domainEvent) where T : IDomainEvent;
}