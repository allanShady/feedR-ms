using FeedR.Aggregator.Services.Models;
using FeedR.Shared.Messaging;

namespace FeedR.Aggregator.Services;

internal sealed class PricingHandler : IPricingHandler
{
    private readonly IMessagePublisher _messagePublisher;
    private readonly ILogger<PricingStreamBackgroundServie> _logger;

    public PricingHandler(IMessagePublisher messagePublisher, ILogger<PricingStreamBackgroundServie> logger)
    {
        _messagePublisher = messagePublisher;
        _logger = logger;
    }
    public Task HandleAsync(CurrencyPair currencyPair)
    {
        throw new NotImplementedException();
    }
}