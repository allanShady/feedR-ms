using FeedR.Shared.Messaging;

namespace FeedR.Shared.Pulsar;

internal sealed class PulsarMessagePublisher : IMessagePublisher
{
    public Task PublishAsync<T>(string topic, T message) where T : IMessage
    {
        throw new NotImplementedException();
    }
}
