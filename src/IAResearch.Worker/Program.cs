using Wolverine;
using Wolverine.RabbitMQ;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddWolverine(opts =>
{
    opts.UseRabbitMqUsingNamedConnection("rabbitmq");
    opts.UseRabbitMq().AutoProvision();
    opts.ListenToRabbitQueue("health-events-created");
    opts.Policies.UseDurableOutboxOnAllSendingEndpoints();
    opts.Policies.UseDurableInboxOnAllListeners();
});

var host = builder.Build();
await host.RunAsync();
