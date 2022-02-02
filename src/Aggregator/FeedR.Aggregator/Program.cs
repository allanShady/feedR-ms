using FeedR.Aggregator.Services;
using FeedR.Shared.Messaging;
using FeedR.Shared.Redis;
using FeedR.Shared.Redis.Streaming;
using FeedR.Shared.Serialization;
using FeedR.Shared.Streaming;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddHostedService<PricingStreamBackgroundServie>()
    .AddHostedService<WeatherStreamBackgroundServie>()
    .AddStreaming()
    .AddSerialization()
    .AddRedis(builder.Configuration)
    .AddRedisStreaming()
    .AddMessaging();

var app = builder.Build();

app.MapGet("/", async ctx =>
{
    await ctx.Response.WriteAsync($"FeedR Aggregator, request ID:, {ctx.Request.Headers["x-request-id"]}");
}
);

app.Run();