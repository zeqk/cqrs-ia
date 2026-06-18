namespace IAResearch.ApiService.Features.HealthEvents.GetHealthEvents;

public sealed record GetHealthEventsQuery(Guid PatientId);

public sealed record GetHealthEventsResponse(IReadOnlyList<GetHealthEventsItem> HealthEvents);

public sealed record GetHealthEventsItem(
    Guid Id,
    Guid PatientId,
    DateTime OccurredAt,
    string Description,
    string Diagnosis,
    string RecommendedTreatment);
