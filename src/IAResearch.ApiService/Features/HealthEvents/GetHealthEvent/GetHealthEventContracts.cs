namespace IAResearch.ApiService.Features.HealthEvents.GetHealthEvent;

public sealed record GetHealthEventQuery(
    Guid PatientId,
    Guid HealthEventId);

public sealed record GetHealthEventResponse(
    Guid Id,
    Guid PatientId,
    DateTime OccurredAt,
    string Description,
    string Diagnosis,
    string RecommendedTreatment);
