using IAResearch.Domain.Events;

public class HealthEventCreatedHandler
{
    public Task Handle(
        HealthEventCreated message,
        ILogger<HealthEventCreatedHandler> logger,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        logger.LogInformation(
            "HealthEventCreated received in Worker. HealthEventId: {HealthEventId}, PatientId: {PatientId}, OccurredAt: {OccurredAt}",
            message.HealthEventId,
            message.PatientId,
            message.OccurredAt);

        return Task.CompletedTask;
    }
}
