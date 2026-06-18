using IAResearch.Infrastructure.Endpoints;
using Wolverine;

namespace IAResearch.ApiService.Features.HealthEvents.GetHealthEvent;

public sealed class GetHealthEventEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/patients/{patientId:guid}/health-events/{healthEventId:guid}", GetHealthEvent)
            .WithName("GetHealthEvent")
            .WithTags("HealthEvents");
    }

    private static async Task<IResult> GetHealthEvent(
        Guid patientId,
        Guid healthEventId,
        IMessageBus messageBus,
        CancellationToken cancellationToken)
    {
        var response = await messageBus.InvokeAsync<GetHealthEventResponse?>(
            new GetHealthEventQuery(patientId, healthEventId),
            cancellationToken);

        return response is null
            ? Results.NotFound()
            : Results.Ok(response);
    }
}
