using FeedR.Feeds.Weather.Services;
using FeedR.Shared.HTTP;

var builder = WebApplication.CreateBuilder(args);
builder
    .Services
    .AddHostedService<WeatherBackgroundService>()
    .AddHttpClient()
    .AddHttpApiClient<IWeatherFeed, WeatherFeed>();

var app = builder.Build();

app.MapGet("/", () => "FeedR Weather feed");

app.Run();