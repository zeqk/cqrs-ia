using IAResearch.Domain;
using Microsoft.EntityFrameworkCore;

namespace IAResearch.ApiService.Persistence;

public static class Extensions
{
    public static IHostApplicationBuilder AddPersistence(this IHostApplicationBuilder applicationBuilder)
    {
        applicationBuilder.Services.AddDbContext<PatientsDbContext>(options =>
        {
            options.UseNpgsql(applicationBuilder.Configuration.GetConnectionString("Default"));

            if (applicationBuilder.Environment.IsDevelopment())
            {

                options.UseAsyncSeeding(async (context, _, cancellationToken) =>
                {
                    var testTicket = await context.Set<Patient>()
                        .FirstOrDefaultAsync(t => t.Id == new Guid("66c30560-6900-4a63-bba5-9367ac419130"));
                    if (testTicket is null)
                    {
                        context.Set<Patient>().Add(new Patient
                        {
                            Id = new Guid("66c30560-6900-4a63-bba5-9367ac419130"),
                            PatientNumber = "P000000001",
                            FirstName = "Juan",
                            LastName = "Jorge",
                            DateOfBirth = new DateOnly(1980, 1, 1)
                        });
                        await context.SaveChangesAsync(cancellationToken);
                    }
                });
            }
        }, optionsLifetime: ServiceLifetime.Singleton);
        return applicationBuilder;
    }

    public static async Task<WebApplication> SeedInitialDataAync(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            // Ensure database is created and seeded
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<PatientsDbContext>();
                await context.Database.EnsureCreatedAsync();
            }
        }
        return app;
    }
}
