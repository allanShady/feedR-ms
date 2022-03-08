using FeedR.Notifier.Services;
using FeedR.Shared.Messaging;
using FeedR.Shared.Pulsar;
using FeedR.Shared.Serialization;
using FeedR.Shared.Observability;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddHttpContextAccessor()
    .AddSerialization()
    .AddMessaging()
    .AddPulsar()
    .AddHostedService<NotifierMessagingBackgroundService>();

var app = builder.Build();
app.UseCorrelationId();

app.MapGet("/", () => "FeedR Notifier");

app.Run();