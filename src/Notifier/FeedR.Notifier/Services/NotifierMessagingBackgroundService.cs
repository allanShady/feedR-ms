using FeedR.Notifier.Events.External;
using FeedR.Shared.Messaging;
using FeedR.Shared.Observability;

namespace FeedR.Notifier.Services;

internal sealed class NotifierMessagingBackgroundService : BackgroundService
{
    private readonly IMessageSubscriber _messageSubscriber;
    private readonly ILogger<NotifierMessagingBackgroundService> _logger;

    public NotifierMessagingBackgroundService(IMessageSubscriber messageSubscriber, ILogger<NotifierMessagingBackgroundService> logger)
    {
        _messageSubscriber = messageSubscriber;
        _logger = logger;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _messageSubscriber.SubscribeAsync<OrderPlaced>("orders", messageEnvelope =>
        {
            _logger.LogInformation($"order with ID: {messageEnvelope.Message.OrderId} for symbol '{messageEnvelope.Message.Symbol}' has been placed" +
            $" Correlation ID: '{messageEnvelope.CorrelationId}'");
        });

        return Task.CompletedTask;
    }
}