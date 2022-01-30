using FeedR.Shared.Streaming;

namespace FeedR.Aggregator.Services;

internal sealed class WeatherStreamBackgroundServie : BackgroundService
{
    private readonly IStreamSubscriber _subscriber;
    private readonly ILogger<PricingStreamBackgroundServie> _logger;
    public WeatherStreamBackgroundServie(IStreamSubscriber subscriber, ILogger<PricingStreamBackgroundServie> logger)
    {
        _logger = logger;
        _subscriber = subscriber;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _subscriber.SubscribeAsync<WeatherData>("weather", weather =>
        {
            _logger.LogInformation($"{weather.Location}:  {weather.Temperature} C, {weather.Humidity} %," +
                 $"{weather.Wind} km/h [{weather.Condition}]");
        });
    }

    private record WeatherData(string Location, double Temperature, double Humidity, double Wind, string Condition);
}