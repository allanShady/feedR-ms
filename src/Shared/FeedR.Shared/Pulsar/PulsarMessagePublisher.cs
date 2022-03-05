using System.Reflection;
using DotPulsar;
using DotPulsar.Abstractions;
using DotPulsar.Extensions;
using FeedR.Shared.Messaging;
using FeedR.Shared.Serialization;
using Microsoft.Extensions.Logging;
using IMessage = FeedR.Shared.Messaging.IMessage;

namespace FeedR.Shared.Pulsar;

internal sealed class PulsarMessagePublisher : IMessagePublisher
{
    //TODO: Extract Pulsar into app setting and dedicated options type
    private readonly ISerializer _serializer;
    private readonly ILogger<PulsarMessagePublisher> _logger;
    private readonly IPulsarClient _client;
    private readonly string _producerName;

    public PulsarMessagePublisher(ISerializer serializer, ILogger<PulsarMessagePublisher> logger)
    {
        _logger = logger;
        _serializer = serializer;
        _client = PulsarClient.Builder().Build();
        _producerName = Assembly.GetEntryAssembly()?.FullName?.Split(",")[0] ?? string.Empty;
    }
    public async Task PublishAsync<T>(string topic, T message) where T : class, IMessage
    {
        var producer = _client.NewProducer()
                            .ProducerName(_producerName)
                            .Topic($"persistent://public/default/{topic}")
                            .Create();

        var payload = _serializer.SerializeBytes(message);
        var messageId = await producer.Send(payload);
        _logger.LogInformation($"Sent a message with ID: '{messageId}'");
    }
}
