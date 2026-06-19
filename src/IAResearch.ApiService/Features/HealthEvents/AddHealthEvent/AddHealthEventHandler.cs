using IAResearch.ApiService.Persistence;
using IAResearch.Domain;
using IAResearch.Domain.Events;
using Microsoft.EntityFrameworkCore;
using Wolverine;

namespace IAResearch.ApiService.Features.HealthEvents.AddHealthEvent;

public static class AddHealthEventHandler
{
    public static async Task<AddHealthEventResponse?> Handle(
        AddHealthEventCommand command,
        PatientsDbContext dbContext,
        IMessageBus messageBus,
        CancellationToken cancellationToken)
    {
        bool patientExists = await dbContext.Patients
            .AsNoTracking()
            .AnyAsync(p => p.Id == command.PatientId, cancellationToken);

        if (!patientExists)
        {
            return null;
        }

        var healthEvent = new HealthEvent
        {
            Id = Guid.NewGuid(),
            PatientId = command.PatientId,
            OccurredAt = command.OccurredAt,
            Description = command.Description,
            Diagnosis = command.Diagnosis,
            RecommendedTreatment = command.RecommendedTreatment
        };

        await dbContext.HealthEvents.AddAsync(healthEvent, cancellationToken);

        await messageBus.PublishAsync(new HealthEventCreated(
            healthEvent.Id,
            healthEvent.PatientId,
            healthEvent.OccurredAt));

        await dbContext.SaveChangesAsync(cancellationToken);

        return new AddHealthEventResponse(
            healthEvent.Id);
    }
}
