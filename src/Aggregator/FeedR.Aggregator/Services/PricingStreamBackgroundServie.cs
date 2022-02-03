using FeedR.Aggregator.Services.Models;
using FeedR.Shared.Streaming;

namespace FeedR.Aggregator.Services;

internal sealed class PricingStreamBackgroundServie : BackgroundService
{
    private readonly IStreamSubscriber _subscriber;
    private readonly IPricingHandler _pricingHandler;
    private readonly ILogger<PricingStreamBackgroundServie> _logger;
    public PricingStreamBackgroundServie(IStreamSubscriber subscriber, IPricingHandler pricingHandler, ILogger<PricingStreamBackgroundServie> logger)
    {
        _logger = logger;
        _subscriber = subscriber;
        _pricingHandler = pricingHandler;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _subscriber.SubscribeAsync<CurrencyPair>("pricing", currencyPair =>
        {
            _logger.LogInformation($"Pricing '{currencyPair.Symbol}' = {currencyPair.Value:F}, timestamp: {currencyPair.TimeStamp}");
            _ = _pricingHandler.HandleAsync(currencyPair);
        });
    }
}