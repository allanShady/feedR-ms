using FeedR.Shared.Messaging;
using FeedR.Shared.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddSerialization()
    .AddMessaging();

var app = builder.Build();

app.MapGet("/", () => "FeedR Notifier");

app.Run();