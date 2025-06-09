using Contracting.Application;
using Contracting.Domain.Abstractions;
using Contracting.Infrastructure;
using Joseco.Outbox.EFCore;
using Nur.Store2025.Observability;

var builder = Host.CreateApplicationBuilder(args);

string serviceName = "contracting.worker-service";

builder.UseLogging(serviceName, builder.Configuration);

builder.Services.AddApplication()
                .AddInfrastructure(builder.Configuration, builder.Environment, serviceName);
builder.Services.AddOutboxBackgroundService<DomainEvent>();

var host = builder.Build();
host.Run();
