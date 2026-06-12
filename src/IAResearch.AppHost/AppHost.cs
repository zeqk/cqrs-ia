var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.IAResearch_ApiService>("apiservice")
    .WithHttpHealthCheck("/health");

builder.AddProject<Projects.IAResearch_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithHttpHealthCheck("/health")
    .WithReference(apiService)
    .WaitFor(apiService);

builder.AddProject<Projects.IAResearch_Worker>("iaresearch-worker");

builder.Build().Run();
