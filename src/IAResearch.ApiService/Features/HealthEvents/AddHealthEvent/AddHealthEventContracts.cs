namespace IAResearch.ApiService.Features.HealthEvents.AddHealthEvent;

public sealed record AddHealthEventRequest(
    DateTime OccurredAt,
    string Description,
    string Diagnosis,
    string RecommendedTreatment);

public sealed record AddHealthEventCommand(
    Guid PatientId,
    DateTime OccurredAt,
    string Description,
    string Diagnosis,
    string RecommendedTreatment);

public sealed record AddHealthEventResponse(Guid Id);

public sealed record HealhEventCreated(
    Guid HealthEventId,
    Guid PatientId,
    DateTime OccurredAt);
