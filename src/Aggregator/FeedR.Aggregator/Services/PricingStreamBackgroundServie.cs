using FeedR.Shared.Streaming;

namespace FeedR.Aggregator.Services;

internal sealed class PricingStreamBackgroundServie : BackgroundService
{
    private readonly IStreamSubscriber _subscriber;
    private readonly ILogger<PricingStreamBackgroundServie> _logger;
    public PricingStreamBackgroundServie(IStreamSubscriber subscriber, ILogger<PricingStreamBackgroundServie> logger)
    {
        _logger = logger;
        _subscriber = subscriber;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _subscriber.SubscribeAsync<CurrencyPair>("pricing", currencyPair =>
        {
            _logger.LogInformation($"Pricing '{currencyPair.Symbol}' = {currencyPair.Value:F}, timestamp: {currencyPair.TimeStamp}");
        });
    }

    private record CurrencyPair(string Symbol, decimal Value, long TimeStamp);
}