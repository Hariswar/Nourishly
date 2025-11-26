using MediatR;

namespace DiningAPI.Events;

public interface IDomainEvent : INotification
{
    DateTime OccurredOn { get; }
}