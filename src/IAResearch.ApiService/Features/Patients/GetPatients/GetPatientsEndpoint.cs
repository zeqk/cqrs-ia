using IAResearch.Infrastructure.Endpoints;
using Wolverine;

namespace IAResearch.ApiService.Features.Patients.GetPatients;

public sealed class GetPatientsEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/patients", Handle)
            .WithName("GetPatients")
            .WithTags("Patients");
    }

    private static async Task<IResult> Handle(
        IMessageBus messageBus,
        CancellationToken cancellationToken)
    {
        GetPatientsResponse response = await messageBus.InvokeAsync<GetPatientsResponse>(
            new GetPatientsQuery(),
            cancellationToken);

        return Results.Ok(response);
    }
}
