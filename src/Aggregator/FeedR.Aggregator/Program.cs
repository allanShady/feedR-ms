using FeedR.Aggregator.Services;
using FeedR.Shared.Messaging;
using FeedR.Shared.Pulsar;
using FeedR.Shared.Redis;
using FeedR.Shared.Redis.Streaming;
using FeedR.Shared.Serialization;
using FeedR.Shared.Streaming;
using FeedR.Shared.Observability;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddHttpContextAccessor()
    .AddHostedService<PricingStreamBackgroundServie>()
    .AddHostedService<WeatherStreamBackgroundServie>()
    .AddStreaming()
    .AddSerialization()
    .AddRedis(builder.Configuration)
    .AddRedisStreaming()
    .AddMessaging()
    .AddPulsar()
    .AddSingleton<IPricingHandler, PricingHandler>();

var app = builder.Build();
app.UseCorrelationId();

app.MapGet("/", async ctx =>
{
    await ctx.Response.WriteAsync($"FeedR Aggregator, request ID:, {ctx.Request.Headers["x-request-id"]}");
}
);

app.Run();