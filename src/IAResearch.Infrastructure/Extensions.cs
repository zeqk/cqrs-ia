using IAResearch.Infrastructure.DomainEvents;
using Microsoft.Extensions.DependencyInjection;

namespace IAResearch.Infrastructure;

public static class Extensions
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IDomainEventsDispatcher, DomainEventsDispatcher>();
    }
}
