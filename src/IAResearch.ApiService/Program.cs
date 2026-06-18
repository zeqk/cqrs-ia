using IAResearch.ApiService.Persistence;
using IAResearch.Infrastructure;
using IAResearch.Infrastructure.Endpoints;
using ImTools;
using JasperFx.CodeGeneration;
using JasperFx.MultiTenancy;
using Microsoft.EntityFrameworkCore;
using Wolverine;
using Wolverine.EntityFrameworkCore;
using Wolverine.Postgresql;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

builder.Services.AddInfrastructureServices();
builder.Services.AddEndpoints(typeof(Program).Assembly);

// Add services to the container.
builder.Services.AddProblemDetails();
builder.AddPersistence();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Host.UseWolverine(opts =>
{
    opts.CodeGeneration.AlwaysUseServiceLocationFor<PatientsDbContext>();


    // You'll need to independently tell Wolverine where and how to 
    // store messages as part of the transactional inbox/outbox
    opts.PersistMessagesWithPostgresql(connectionString: builder.Configuration.GetConnectionString("Default"));

    // Adding EF Core transactional middleware, saga support,
    // and EF Core support for Wolverine storage operations
    opts.UseEntityFrameworkCoreTransactions();


    opts.Policies.UseDurableOutboxOnAllSendingEndpoints();
    opts.Policies.UseDurableInboxOnAllListeners();
});

var app = builder.Build();

await app.SeedInitialDataAync();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUi(cfg =>
    {
        cfg.DocumentPath = "/openapi/v1.json";
    });
}

string[] summaries = ["Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"];

app.MapGet("/", () => "API service is running. Navigate to /weatherforecast to see sample data.");

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.MapDefaultEndpoints();
app.MapEndpoints();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
