using IAResearch.ApiService.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IAResearch.ApiService.Features.HealthEvents.GetHealthEvents;

public static class GetHealthEventsHandler
{
    public static async Task<GetHealthEventsResponse> Handle(
        GetHealthEventsQuery query,
        PatientsDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var healthEvents = await dbContext.HealthEvents
            .AsNoTracking()
            .Where(h => h.PatientId == query.PatientId)
            .OrderByDescending(h => h.OccurredAt)
            .ThenBy(h => h.Id)
            .Select(h => new GetHealthEventsItem(
                h.Id,
                h.PatientId,
                h.OccurredAt,
                h.Description,
                h.Diagnosis,
                h.RecommendedTreatment))
            .ToListAsync(cancellationToken);

        return new GetHealthEventsResponse(healthEvents);
    }
}
