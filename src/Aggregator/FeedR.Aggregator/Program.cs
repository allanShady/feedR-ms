using FeedR.Aggregator.Services;
using FeedR.Shared.Redis;
using FeedR.Shared.Streaming;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddSingleton<PricingStreamBackgroundServie>()
    .AddStreaming()
    .AddRedis(builder.Configuration);

var app = builder.Build();

app.MapGet("/", () => "FeedR Aggregator");

app.Run();