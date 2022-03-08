using System.Reflection;
using DotPulsar;
using DotPulsar.Extensions;
using FeedR.Shared.Messaging;
using FeedR.Shared.Serialization;
using Microsoft.Extensions.Logging;

namespace FeedR.Shared.Pulsar;

internal sealed class PulsarMessageSubscriber : IMessageSubscriber
{
    private readonly string _consumerName;
    private readonly ISerializer _serializer;
    private readonly ILogger<PulsarMessageSubscriber> _logger;
    private readonly DotPulsar.Abstractions.IPulsarClient _client;

    public PulsarMessageSubscriber(ISerializer serializer, ILogger<PulsarMessageSubscriber> logger)
    {
        _logger = logger;
        _serializer = serializer;
        _client = PulsarClient.Builder().Build();
        _consumerName = Assembly.GetEntryAssembly()?.FullName?.Split(",")[0] ?? string.Empty;
    }
    public async Task SubscribeAsync<T>(string topic, Action<MessageEnvelope<T>> handler) where T : class, IMessage
    {
        var subscription = $"{_consumerName}-{topic}";
        var consumer = _client.NewConsumer()
            .SubscriptionName(subscription)
            .Topic($"persistent://public/default/{topic}")
            .Create();

        await foreach (var message in consumer.Messages())
        {
            var producer = message.Properties["producer"];
            var customId = message.Properties["custom_id"];
            var correlationId = message.Properties["correlationId"];

            _logger.LogInformation($"Received a message with ID: '{message.MessageId}' from: '{producer}' with custom ID '{customId}'");
            var payload = _serializer.DeserializeBytes<T>(message.Data.FirstSpan.ToArray());

            if (payload is not null)
            {
                var json = _serializer.Serialize(payload);
                _logger.LogInformation(json);
                handler(new MessageEnvelope<T>(payload, correlationId));
            }

            await consumer.Acknowledge(message);
        }
    }
}
