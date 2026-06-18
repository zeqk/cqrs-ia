var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("hdata")
                    .WithPgWeb(cfg => cfg.WithHostPort(55403));
var postgresdb = postgres.AddDatabase("hdatadb");

var apiService = builder.AddProject<Projects.IAResearch_ApiService>("apiservice")
    .WaitFor(postgresdb)
    .WithReference(postgresdb, connectionName: "Default")
    .WithHttpHealthCheck("/health");

builder.AddProject<Projects.IAResearch_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithHttpHealthCheck("/health")
    .WithReference(apiService)
    .WaitFor(apiService);

builder.AddProject<Projects.IAResearch_Worker>("worker");

builder.Build().Run();
