namespace DiningAPI.Events;

public record LocationAccessedEvent(int LocationId, string LocationName, DateTime OccurredOn) : IDomainEvent;