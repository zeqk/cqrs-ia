namespace IAResearch.Domain.Events;

public sealed record HealthEventCreated(
    Guid HealthEventId,
    Guid PatientId,
    DateTime OccurredAt) : IDomainEvent;
