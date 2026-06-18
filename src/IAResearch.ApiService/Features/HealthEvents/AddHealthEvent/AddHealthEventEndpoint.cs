using IAResearch.Infrastructure.Endpoints;
using Wolverine;

namespace IAResearch.ApiService.Features.HealthEvents.AddHealthEvent;

public sealed class AddHealthEventEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("api/patients/{patientId:guid}/health-events", AddHealthEvent)
            .WithName("AddHealthEvent")
            .WithTags("HealthEvents");
    }

    private static async Task<IResult> AddHealthEvent(
        Guid patientId,
        AddHealthEventRequest request,
        IMessageBus messageBus,
        CancellationToken cancellationToken)
    {
        var response = await messageBus.InvokeAsync<AddHealthEventResponse?>(
            new AddHealthEventCommand(
                patientId,
                request.OccurredAt,
                request.Description,
                request.Diagnosis,
                request.RecommendedTreatment),
            cancellationToken);

        if (response is null)
        {
            return Results.NotFound();
        }

        return Results.Created($"/api/patients/{patientId}/health-events/{response.Id}", response);
    }
}
