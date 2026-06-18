using IAResearch.Infrastructure.Endpoints;
using Wolverine;

namespace IAResearch.ApiService.Features.HealthEvents.GetHealthEvents;

public sealed class GetHealthEventsEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/patients/{patientId:guid}/health-events", GetHealthEvents)
            .WithName("GetHealthEvents")
            .WithTags("HealthEvents");
    }

    private static async Task<IResult> GetHealthEvents(
        Guid patientId,
        IMessageBus messageBus,
        CancellationToken cancellationToken)
    {
        var response = await messageBus.InvokeAsync<GetHealthEventsResponse>(
            new GetHealthEventsQuery(patientId),
            cancellationToken);

        return Results.Ok(response);
    }
}
