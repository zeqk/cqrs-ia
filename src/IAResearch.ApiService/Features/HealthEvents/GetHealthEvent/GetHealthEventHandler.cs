using IAResearch.ApiService.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IAResearch.ApiService.Features.HealthEvents.GetHealthEvent;

public static class GetHealthEventHandler
{
    public static async Task<GetHealthEventResponse?> Handle(
        GetHealthEventQuery query,
        PatientsDbContext dbContext,
        CancellationToken cancellationToken)
    {
        return await dbContext.HealthEvents
            .AsNoTracking()
            .Where(h => h.PatientId == query.PatientId && h.Id == query.HealthEventId)
            .Select(h => new GetHealthEventResponse(
                h.Id,
                h.PatientId,
                h.OccurredAt,
                h.Description,
                h.Diagnosis,
                h.RecommendedTreatment))
            .FirstOrDefaultAsync(cancellationToken);
    }
}
