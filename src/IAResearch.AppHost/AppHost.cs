var builder = DistributedApplication.CreateBuilder(args);

var rabbitMqUsername = builder.AddParameter("username", value: "guest", secret: true);
var rabbitMqPassword = builder.AddParameter("password", value: "guest", secret: true);

var rabbitmq = builder.AddRabbitMQ("rabbitmq", rabbitMqUsername, rabbitMqPassword)
    .WithManagementPlugin(port: 15672); 

var postgres = builder.AddPostgres("hdata")
                    .WithPgWeb(cfg => cfg.WithHostPort(55403));
var postgresdb = postgres.AddDatabase("hdatadb");

var apiService = builder.AddProject<Projects.IAResearch_ApiService>("apiservice")
    .WaitFor(postgresdb)
    .WaitFor(rabbitmq)
    .WithReference(postgresdb, connectionName: "Default")
    .WithReference(rabbitmq)
    .WithHttpHealthCheck("/health");

builder.AddProject<Projects.IAResearch_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithHttpHealthCheck("/health")
    .WithReference(apiService)
    .WaitFor(apiService);

builder.AddProject<Projects.IAResearch_Worker>("worker")
    .WaitFor(rabbitmq)
    .WithReference(rabbitmq);

builder.Build().Run();
