using Microsoft.AspNetCore.Routing;

namespace IAResearch.Infrastructure.Endpoints;

public interface IEndpoint
{    void MapEndpoint(IEndpointRouteBuilder app);
}
