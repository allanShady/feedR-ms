using FeedR.Shared.Redis;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRedis(builder.Configuration);

var app = builder.Build();

app.MapGet("/", () => "FeedR Aggregator");

app.Run();