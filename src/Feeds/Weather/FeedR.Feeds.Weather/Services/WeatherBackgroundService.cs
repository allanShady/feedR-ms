namespace FeedR.Feeds.Weather.Services;

internal sealed class WeatherBackgroundService : BackgroundService
{
    private readonly IWeatherFeed _weatherFeed;
    private readonly ILogger<WeatherBackgroundService> _logger;

    public WeatherBackgroundService(IWeatherFeed weatherFeed, ILogger<WeatherBackgroundService> logger)
    {
        _weatherFeed = weatherFeed;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (var weather in _weatherFeed.SubscribeAsync("Nampula", stoppingToken))
            _logger.LogInformation($"{weather.Location}:  {weather.Temperature} C, {weather.Humidity} %," +
            $"{weather.Wind} km/h [{weather.Condition}]");
    }
}