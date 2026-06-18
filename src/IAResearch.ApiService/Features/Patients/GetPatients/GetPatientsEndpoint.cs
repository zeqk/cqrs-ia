using IAResearch.Infrastructure.Endpoints;
using Wolverine;

namespace IAResearch.ApiService.Features.Patients.GetPatients;

public sealed class GetPatientsEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/patients", GetPatients)
            .WithName("GetPatients")
            .WithTags("Patients");
    }

    private static async Task<IResult> GetPatients(
        IMessageBus messageBus,
        CancellationToken cancellationToken)
    {
        var response = await messageBus.InvokeAsync<GetPatientsResponse>(
            new GetPatientsQuery(),
            cancellationToken);

        return Results.Ok(response);
    }
}
